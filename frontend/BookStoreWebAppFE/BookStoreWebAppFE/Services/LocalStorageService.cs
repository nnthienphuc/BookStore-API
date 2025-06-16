using BookStoreWebAppFE.Components.Helper;
using BookStoreWebAppFE.Components.Share;
using Microsoft.JSInterop;
using System.Text.Json;

namespace BookStoreWebAppFE.Services
{
    public interface ILocalStorageService
    {
        Task<T> GetItem<T>(string key);
        Task SetItemAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task RemoveItemAsync(string key);
        Task ClearAllAsync();
    }

    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<T> GetItem<T>(string key)
        {
            string json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            if (json == null) return default;

            StorageItem<string>? storageItem = JsonSerializer.Deserialize<StorageItem<string>>(json);
            if (storageItem is null) return default;

            if (storageItem.Expiry != null && storageItem.Expiry.Value < DateTime.UtcNow)
            {
                await RemoveItemAsync(key);
                return default;
            }
            return EncryptionHelper.DecryptAndDeserialize<T>(storageItem.Data);
        }

        public async Task SetItemAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            if (value is null) return;

            string encryptedData = EncryptionHelper.SerializeAndEncrypt(value);

            StorageItem<string> storageItem = new()
            {
                Data = encryptedData // Lưu dữ liệu đã mã hóa dưới dạng string
            };

            if (expiry != null)
            {
                storageItem.Expiry = DateTime.UtcNow.Add(expiry.Value);
            }
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(storageItem));
        }


        public async Task RemoveItemAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task ClearAllAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.clear");
        }
    }
}
