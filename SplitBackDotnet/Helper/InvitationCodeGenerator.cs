using System.Security.Cryptography;
namespace SplitBackDotnet.Helper;

public static class InvitationCodeGenerator
{
  public static string GenerateInvitationCode()
  {
    using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
    {
      byte[] data = new byte[4];
      rng.GetBytes(data);
      uint randomInt = BitConverter.ToUInt32(data, 0);
      return randomInt.ToString("X8");
    }
  }
}