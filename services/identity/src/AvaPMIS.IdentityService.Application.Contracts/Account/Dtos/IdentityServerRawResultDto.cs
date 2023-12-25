namespace AvaPMIS.IdentityService.Account.Dtos
{
    public class AccessTokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpireIn { get; set; }

    }
    public class IdentityServerRawResultDto
    {
        public int Status { get; set; }
        public bool IsError { get; set; } = false;
        public string ErrorDescription { get; set; }
        public AccessTokenDto Token { get; set; }
    }
}