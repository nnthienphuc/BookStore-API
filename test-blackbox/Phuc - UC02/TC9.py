# Thể loại rỗng
from selenium import webdriver
from selenium.webdriver.edge.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
import time

EDGE_DRIVER_PATH = "edgedriver_win64\\msedgedriver.exe"
service = Service(EDGE_DRIVER_PATH)
driver = webdriver.Edge(service=service)

try:
    wait = WebDriverWait(driver, 15)
    driver.get("https://localhost:7225")
    time.sleep(2)

    print("🧪 Đang điền thông tin đăng nhập...")

    email_input = wait.until(EC.presence_of_element_located((By.XPATH, "//div[@class='form-container sign-in-container']//input[@placeholder='Email']")))
    email_input.clear()
    email_input.send_keys("phucnaoto@gmail.com")
    time.sleep(1)

    password_input = wait.until(EC.presence_of_element_located((By.XPATH, "//div[@class='form-container sign-in-container']//input[@placeholder='Password']")))
    password_input.clear()
    password_input.send_keys("123456")
    time.sleep(1.5)

    sign_in_btn = wait.until(EC.element_to_be_clickable((By.XPATH, "//div[@class='form-container sign-in-container']//button[contains(text(), 'Sign In')]")))
    sign_in_btn.click()
    time.sleep(3)

    wait.until(EC.url_contains("/admin/book"))
    print("✅ Đăng nhập thành công.")

    wait.until(EC.presence_of_element_located((By.XPATH, "//table")))
    time.sleep(1)

    create_btn = wait.until(EC.element_to_be_clickable((By.XPATH, "//button[span[text()='Tạo mới']]")))
    create_btn.click()
    print("🟢 Đã click Create")
    time.sleep(3)

    print("📝 Điền thông tin sách...")

    # ISBN
    isbn_input = wait.until(EC.presence_of_element_located((By.ID, "Isbn")))
    isbn_input.send_keys("8935244884708")
    print("✅ Đã điền ISBN.")
    time.sleep(1.5)

    # Title
    title_input = driver.find_element(By.ID, "Title")
    title_input.send_keys("Doraemon - Truyện Dài - Tập 4 - Nobita Và Lâu Đài Dưới Đáy Biển")
    print("✅ Đã điền Title.")
    time.sleep(1.5)

    # Category
    # category_input = driver.find_element(By.XPATH, "//input[@name='CategoryComboboxId']")
    # category_input.send_keys("Truyện Tranh")
    # category_input.send_keys(Keys.ENTER)
    # print("✅ Đã điền Category.")
    # time.sleep(1.5)
    print("❌ Không điền Category vì Category rỗng.")

    # Author
    author_input = driver.find_element(By.XPATH, "//input[@name='AuthorComboboxId']")
    author_input.send_keys("Fujiko F Fujio")
    author_input.send_keys(Keys.ENTER)
    print("✅ Đã điền Author.")
    time.sleep(1.5)

    # Publisher
    publisher_input = driver.find_element(By.XPATH, "//input[@name='PublisherComboboxId']")
    publisher_input.send_keys("Nhà xuất bản Trẻ")
    publisher_input.send_keys(Keys.ENTER)
    print("✅ Đã điền Publisher.")
    time.sleep(1.5)

    # Year of Publication
    year_input = driver.find_element(By.XPATH, "//input[@name='YearOfPublication']")
    year_input.send_keys("2023")
    print("✅ Đã điền Year of Publication.")
    time.sleep(1.5)

    # Quantity
    quantity_input = driver.find_element(By.ID, "Quantity")
    quantity_input.send_keys("10")
    print("✅ Đã điền Quantity.")
    time.sleep(1.5)

    # Price
    price_input = driver.find_element(By.ID, "Price")
    price_input.send_keys("150000")
    print("✅ Đã điền Price.")
    time.sleep(1.5)

    # Image
    image_input = driver.find_element(By.ID, "Image")
    image_input.send_keys("https://cdn1.fahasa.com/media/catalog/product/d/o/doraemon-truyen-dai---nobita-va-lau-dai-duoi-day-bien---tap-4---tb-2023.jpg")
    print("✅ Đã điền Image.")
    time.sleep(1.5)

    # Save
    save_button = wait.until(EC.element_to_be_clickable((By.XPATH, "//button[span[text()='Save']]")))
    save_button.click()
    print("✅ Đã gửi thông tin sách.")

    # time.sleep(6)

    # Giữ edge vẫn mở để quan sát
    print("\n🚀 Test hoàn tất! Trình duyệt sẽ giữ nguyên để quan sát.")
    input("Nhấn Enter để đóng trình duyệt...")

except Exception as e:
    print(f"❌ Lỗi xảy ra: {e}")

finally:
    driver.quit()
