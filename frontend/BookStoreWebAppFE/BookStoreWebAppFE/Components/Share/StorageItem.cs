namespace BookStoreWebAppFE.Components.Share
{
    public class StorageItem<T>
    {
        /// <summary>
        /// data cần lưu vào store
        /// </summary>
        public required T Data { get; set; }

        /// <summary>
        /// time hết hạn
        /// </summary>
        public DateTime? Expiry { get; set; }
    }
}
