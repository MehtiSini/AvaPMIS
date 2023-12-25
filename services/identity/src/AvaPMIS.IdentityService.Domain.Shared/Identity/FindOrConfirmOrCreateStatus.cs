namespace AvaPMIS.IdentityService.Identity
{
    public enum FindOrConfirmOrCreateStatus
    {
        None = 0,
        ExistedAndConfimed = 1,
        NotExistedAndCreated = 2,
        ExistedButNutConfirmed = 3
    }
}