namespace API.Resources.DTOs.Pay;

public class Receivables
{
    #region Property
    public float PIT { get; } // Personal income tax
    public float SocialInsurance { get; } = 5; // unit %
    public float HealthInsurance { get; } = 1; // unit %
    #endregion

    #region Constructor
    public Receivables(decimal gross)
    {
            if (0 < gross && gross <= 5_000_000m)
                this.PIT = 5f;
            if (5_000_000m < gross && gross <= 10_000_000m)
                this.PIT = 10f;
            if (10_000_000m < gross && gross <= 18_000_000m)
                this.PIT = 15f;
            if (18_000_000m < gross && gross <= 32_000_000m)
                this.PIT = 20f;
            if (32_000_000m < gross && gross <= 52_000_000m)
                this.PIT = 25f;
            if (52_000_000m < gross && gross <= 80_000_000m)
                this.PIT = 30f;
            if (gross > 80_000_000m)
                this.PIT = 35f;
        }
    #endregion
}