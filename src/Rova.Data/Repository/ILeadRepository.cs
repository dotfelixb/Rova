using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Rova.Model;
using Rova.Model.Domain;

namespace Rova.Data.Repository
{
    public interface ILeadRepository
    {
        Task<IEnumerable<LeadExtended>> List(int offset = 0, int limit = 1000);
        Task<long> GenerateLeadCode();
        Task<int> CreateLead(Lead lead, DbAuditLog auditLog);
    }

    public class LeadRepository 
        : BasePostgresRepository, ILeadRepository
    {
        public LeadRepository(
            IOptions<ConnectionStringOptions> options) 
            : base(options)
        {
        }

        public Task<IEnumerable<LeadExtended>> List(int offset = 0, int limit = 1000)
        {
            return WithConnection(conn =>
            {
                const string query = @"SELECT id, code, title, firstname
                                , lastname, birthat, gender, displayname
                                , company, phone, mobile, website
                                , email, campaign, (SELECT displayname FROM campaign WHERE id = l.campaign) AS campaignname 
                                , status, leadtype, addresstype, addressstreet, addresscity
                                , addressstate, marketsegment, industry, subscribed
                                , deleted, createdby, createdat, updatedby, updatedat 
                            FROM public.lead l
                            OFFSET @Offset LIMIT @Limit;";

                return conn.QueryAsync<LeadExtended>(query, new
                {
                    Offset = offset, 
                    Limit = limit
                });
            });
        }

        public Task<long> GenerateLeadCode()
        {
            return WithConnection(conn =>
            {
                var query = "SELECT NEXTVAL('LeadCode');";

                return conn.QuerySingleAsync<long>(query);
            });
        }

        public Task<int> CreateLead(Lead lead, DbAuditLog auditLog)
        {
            return WithConnection(async conn =>
            {
                var leadCmd = @"INSERT INTO public.lead (
                                    id, code, title, firstname, lastname, birthat, gender
                                    , displayname, company, phone, mobile, website
                                    , email, campaign, leadtype, addresstype
                                    , addressstreet, addresscity, addressstate, marketsegment
                                    , industry, subscribed, createdby, updatedby
                                )
                                VALUES(@Id, @Code, @Title, @FirstName, @LastName, @BirthAt, @Gender
                                    , @DisplayName, @Company, @Phone, @Mobile, @Website
                                    , @Email, @Campaign, @LeadType, @AddressType
                                    , @AddressStreet, @AddressCity, @AddressState, @Market
                                    , @Industry, @Subscribed, @CreatedBy, @UpdatedBy   
                                )";

                var createLead = await conn.ExecuteAsync(leadCmd, new
                {
                    lead.Id,
                    lead.Code,
                    lead.Title,
                    lead.FirstName,
                    lead.LastName,
                    lead.BirthAt,
                    lead.Gender,
                    lead.DisplayName,
                    lead.Company,
                    lead.Phone,
                    lead.Mobile,
                    lead.Website,
                    lead.Email,
                    lead.Campaign,
                    lead.LeadType,
                    lead.AddressType,
                    lead.AddressStreet,
                    lead.AddressCity,
                    lead.AddressState,
                    lead.Market,
                    lead.Industry,
                    lead.Subscribed,
                    lead.CreatedBy,
                    lead.UpdatedBy
                });

                var logCmd = @"INSERT INTO public.leadauditlog (
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

                return (createLead + createLog);
            });
        }
    }
}