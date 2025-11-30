namespace Spoleto.TrueApi
{
    /// <summary>
    /// Типы изображения. Используется в полной информации о товаре
    /// </summary>
    public enum NkProductPhotoType
    {
        /// <summary>
        /// фотография по умолчанию (вид спереди)
        /// </summary>
        @default,

        /// <summary>
        /// crop-фотография для планограмм(обрезанная по контуру товара)
        /// </summary>
        facing,

        /// <summary>
        /// фотография товара слева 
        /// </summary>
        left,

        /// <summary>
        /// фотография товара справа
        /// </summary>
        right,

        /// <summary>
        /// фотография товара сзади
        /// </summary>
        back,

        /// <summary>
        /// 3D серия - 3ds
        /// </summary>
        series3ds,

        /// <summary>
        /// коммерческая фотография товара
        /// </summary>
        marketing,

        /// <summary>
        /// e-commerce фото
        /// </summary>
        ecommerce,

        /// <summary>
        /// single shot, фотография товара с непредопределенного ракурса
        /// </summary>
        undef,

        /// <summary>
        /// фотография измерения ВГХ
        /// </summary>
        cubi
    }
}
