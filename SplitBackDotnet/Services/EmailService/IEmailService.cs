using SplitBackDotnet.Dtos;

namespace SplitBackDotnet.Services.EmailService;

public interface IEmailService {
  void SendEmail(EmailDto request);
}
