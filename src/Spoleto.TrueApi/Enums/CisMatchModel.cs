namespace Spoleto.TrueApi
{
    /// <summary>
    /// Режимы фильтрации по КИ
    /// </summary>
    public enum CisMatchModel
    {
        /// <summary>
        /// полностью равен
        /// </summary>
        EQ,

        /// <summary>
        /// содержит в себе
        /// </summary>
        LIKE,

        /// <summary>
        /// начинается с
        /// </summary>
        Start_with
    }
}
