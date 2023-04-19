using Api.Model;

namespace Api.DataAccess
{
    public interface IEmailService
    {
        Task<ResponseModel<bool>> SendEmail(EmailParam emailParameters);
    }
}
