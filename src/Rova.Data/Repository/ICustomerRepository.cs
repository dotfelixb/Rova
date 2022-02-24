using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using Rova.Model;
using Rova.Model.Domain;

namespace Rova.Data.Repository
{
    public interface ICustomerRepository
    {
        Task<CustomerExtended> Get(Guid customerId);
        Task<IEnumerable<CustomerExtended>> List(int offset, int limit);
        Task<long> GenerateCustomerCode();
        Task<int> CreateCustomer(Customer customer, DbAuditLog auditLog);
        Task<int> Log(DbAuditLog auditLog);
    }

    public class CustomerRepository : BasePostgresRepository, ICustomerRepository
    {
        public CustomerRepository(
            IOptions<ConnectionStringOptions> options)
            : base(options)
        {
        }

        public Task<CustomerExtended> Get(Guid customerId)
        {
            return WithConnection(async conn =>
            {
                var query = @"SELECT id, code, title, firstname
                                , lastname, birthat, gender, displayname
                                , company, phone, mobile, website
                                , email, fromlead, (SELECT displayname FROM lead WHERE id = fromlead)  AS fromleadname
                                , customertype, subcustomer, parentcustomer
                                , (SELECT displayname FROM customer WHERE id = parentcustomer)  AS parentcustomername
                                , billparent, isinternal, billingstreet
                                , billingcity, billingstate, shippingstreet, shippingcity
                                , shippingstate, shipbilling, preferredmethod, preferreddelivery
                                , openingbalance, openingbalanceat, taxid, taxexempted
                                , deleted, createdby, createdat, updatedby, updatedat
                            FROM public.customer
                            WHERE id = @CustomerId;
                            ";

                return await conn.QueryFirstOrDefaultAsync<CustomerExtended>(query, new
                {
                    CustomerId = customerId
                });
            });
        }

        public Task<IEnumerable<CustomerExtended>> List(int offset = 0, int limit = 1000)
        {
            return WithConnection(conn =>
            {
                var query = @"SELECT id, code, title, firstname
                                , lastname, birthat, gender, displayname
                                , company, phone, mobile, website
                                , email, fromlead, (SELECT displayname FROM lead WHERE id = fromlead)  AS fromleadname
                                , customertype, subcustomer, parentcustomer
                                , (SELECT displayname FROM customer WHERE id = parentcustomer)  AS parentcustomername
                                , billparent, isinternal, billingstreet
                                , billingcity, billingstate, shippingstreet, shippingcity
                                , shippingstate, shipbilling, preferredmethod, preferreddelivery
                                , openingbalance, openingbalanceat, taxid, taxexempted
                                , deleted, createdby, createdat, updatedby, updatedat
                            FROM public.customer
                            OFFSET @Offset LIMIT @Limit;
                            ";

                return conn.QueryAsync<CustomerExtended>(query, new
                {
                    Offset = offset,
                    Limit = limit
                });
            });
        }

        public Task<long> GenerateCustomerCode()
        {
            return WithConnection(conn =>
            {
                var query = "SELECT NEXTVAL('CustomerCode');";

                return conn.QuerySingleAsync<long>(query);
            });
        }

        public Task<int> CreateCustomer(Customer customer, DbAuditLog auditLog)
        {
            return WithConnection(async conn =>
            {
                var custCmd = @"INSERT INTO public.customer (
                                id, code, title, firstname, lastname, birthat, gender
                                , displayname, company, phone, mobile, website
                                , email, fromlead, customertype, subcustomer
                                , parentcustomer, billparent, isinternal, billingstreet
                                , billingcity, billingstate, shippingstreet, shippingcity
                                , shippingstate, shipbilling, preferredmethod, preferreddelivery
                                , openingbalance, openingbalanceat, taxid, taxexempted
                                , createdby, updatedby
                            )
                            VALUES(
                                @Id, @Code, @Title, @FirstName, @LastName, @BirthAt, @Gender
                                , @DisplayName, @Company, @Phone, @Mobile, @Website
                                , @Email, @FromLead, @CustomerType, @SubCustomer
                                , @ParentCustomer, @BillParent, @IsInternal, @BillingStreet
                                , @BillingCity, @BillingState, @ShippingStreet, @ShippingCity
                                , @ShippingState, @ShipBilling, @PreferredMethod, @PreferredDelivery
                                , @OpeningBalance, @OpeningBalanceAt, @TaxId, @TaxExempted
                                , @CreatedBy, @UpdatedBy
                            );";


                var createdCustomer = await conn.ExecuteAsync(custCmd, new
                {
                    customer.Id,
                    customer.Code,
                    customer.Title,
                    customer.FirstName,
                    customer.LastName,
                    customer.BirthAt,
                    customer.Gender,
                    customer.DisplayName,
                    customer.Company,
                    customer.Phone,
                    customer.Mobile,
                    customer.Website,
                    customer.Email,
                    customer.FromLead,
                    customer.CustomerType,
                    customer.SubCustomer,
                    customer.ParentCustomer,
                    customer.BillParent,
                    customer.IsInternal,
                    customer.BillingStreet,
                    customer.BillingCity,
                    customer.BillingState,
                    customer.ShippingStreet,
                    customer.ShippingCity,
                    customer.ShippingState,
                    customer.ShipBilling,
                    customer.PreferredMethod,
                    customer.PreferredDelivery,
                    customer.OpeningBalance,
                    customer.OpeningBalanceAt,
                    customer.TaxId,
                    customer.TaxExempted,
                    customer.CreatedBy,
                    customer.UpdatedBy,

                });

                var logCmd = @"INSERT INTO public.customerauditlog (
                                id, targetid, actionname, objectname
                                , objectdata, createdby
                            )
                            VALUES(@Id, @TargetId, @ActionName, @ObjectName
                                , @ObjectData::jsonb, @CreatedBy
                            );";

                var createLog = await conn.ExecuteAsync(logCmd, new
                {
                    auditLog.Id,
                    auditLog.TargetId,
                    auditLog.ActionName,
                    auditLog.ObjectName,
                    auditLog.ObjectData,
                    auditLog.CreatedBy
                });

                return (createdCustomer + createLog);
            });
        }

        public Task<int> Log(DbAuditLog auditLog)
        {
            return WithConnection(conn =>
            {
                var cmd = @"INSERT INTO public.customerauditlog (
                                id, targetid, actionname, objectname
                                , objectdata, createdby
                            )
                            VALUES(@Id, @TargetId, @ActionName, @ObjectName
                                , @ObjectData::jsonb, @CreatedBy
                            );";

                return conn.ExecuteAsync(cmd, new
                {
                    auditLog.Id,
                    auditLog.TargetId,
                    auditLog.ActionName,
                    auditLog.ObjectName,
                    auditLog.ObjectData,
                    auditLog.CreatedBy
                });
            });
        }
    }
}

