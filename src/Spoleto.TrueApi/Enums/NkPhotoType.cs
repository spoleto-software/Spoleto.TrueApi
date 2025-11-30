namespace Spoleto.TrueApi
{
    /// <summary>
    /// Типы изображения
    /// </summary>
    public enum NkPhotoType
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
        /// фотография товара слева - 7
        /// </summary>
        left7,

        /// <summary>
        /// фотография товара справа - 19
        /// </summary>
        right19,

        /// <summary>
        /// фотография товара сзади - 13
        /// </summary>
        back13,

        /// <summary>
        /// фотография товара сверху
        /// </summary>
        si1,

        /// <summary>
        /// фотография товара снизу
        /// </summary>
        si2,

        /// <summary>
        /// фотография товара в упаковке
        /// </summary>
        si3,

        /// <summary>
        /// фотография товара без упаковки
        /// </summary>
        si4,

        /// <summary>
        /// фотография товара внутри упаковки
        /// </summary>
        si5,

        /// <summary>
        /// 3D серия - 3ds
        /// </summary>
        series3ds,

        /// <summary>
        /// коммерческая фотография товара
        /// </summary>
        marketing,

        /// <summary>
        /// фотография текста на товаре
        /// </summary>
        text,

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
