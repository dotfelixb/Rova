using System;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using Rova.Model;
using Rova.Model.Domain;

namespace Rova.Data.Repository
{
    public interface ICustomerRepository
    {
        Task<int> Create(Customer customer, DbAuditLog auditLog);
        Task<int> Log(DbAuditLog auditLog);
    }

    public class CustomerRepository : BasePostgresRepository, ICustomerRepository
    {
        public CustomerRepository(
            IOptions<ConnectionStringOptions> options)
            : base(options)
        {
        }

        public Task<int> Create(Customer customer, DbAuditLog auditLog)
        {
            return WithConnection(conn =>
            {
                var cmd = @"INSERT INTO public.customer (
                                id, title, firstname, lastname, birthat, gender
                                , displayname, company, phone, mobile, website
                                , email, fromlead, customertype, subcustomer
                                , parentcustomer, billparent, isinternal, billingstreet
                                , billingcity, billingstate, shippingstreet, shippingcity
                                , shippingstate, shipbilling, preferredmethod, preferreddelivery
                                , openingbalance, openingbalanceat, taxid, taxexempted
                                , createdby, updatedby
                            )
                            VALUES(
                                @Id, @Title, @FirstName, @LastName, @BirthAt, @Gender
                                , @DisplayName, @Company, @Phone, @Mobile, @Website
                                , @Email, @FromLead, @CustomerType, @SubCustomer
                                , @ParentCustomer, @BillParent, @IsInternal, @BillingStreet
                                , @BillingCity, @BillingState, @ShippingStreet, @ShippingCity
                                , @ShippingState, @ShipBilling, @PreferredMethod, @PreferredDelivery
                                , @OpeningBalance, @OpeningBalanceAt, @TaxId, @TaxExempted
                                , @CreatedBy, @UpdatedBy
                            );

                            INSERT INTO public.customerauditlog (
                                id, targetid, actionname, objectname
                                , objectdata, createdby
                            )
                            VALUES(@AuditId, @TargetId, @ActionName, @ObjectName
                                , @ObjectData::jsonb, @AuditCreatedBy
                            );
";

                return conn.ExecuteAsync(cmd, new
                {
                    customer.Id,
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

                    AuditId = auditLog.Id,
                    auditLog.TargetId,
                    auditLog.ActionName,
                    auditLog.ObjectName,
                    auditLog.ObjectData,
                    AuditCreatedBy = auditLog.CreatedBy
                });
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

