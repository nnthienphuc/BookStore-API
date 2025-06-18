from selenium import webdriver
from selenium.webdriver.edge.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.keys import Keys
import time

# Đường dẫn Edge WebDriver
EDGE_DRIVER_PATH = "Phuc - UC02\\edgedriver_win64\\msedgedriver.exe"

# Khởi động WebDriver
service = Service(EDGE_DRIVER_PATH)
driver = webdriver.Edge(service=service)
try:
    # vào trang đăng nhập
    driver.get("https://localhost:7225/")
    time.sleep(3)

    # Điền thông tin đăng nhập
    driver.find_element(By.ID, "email").send_keys("phucnaoto@gmail.com")
    driver.find_element(By.ID, "password").send_keys("123456")
    time.sleep(3)

    # click btn login
    driver.find_element(By.XPATH, "//button[text()='Sign In']").click()
    time.sleep(10)

    # Chờ 5s
    wait = WebDriverWait(driver, 5)

    # Lấy ô tìm kiếm title theo aria-label
    title_search = wait.until(EC.element_to_be_clickable(
        (By.XPATH, "//input[@aria-label='Specify the search value for Tiêu đề field']")
    ))

    # Xóa trước khi nhập nếu cần
    title_search.clear()

    # Nhập từ khóa tìm kiếm
    title_search.send_keys("Kính vạn hoa")

    # Nhấn Enter để tìm kiếm
    title_search.send_keys(Keys.ENTER)

    # chọn sách  vi tri thu 1
    checkbox = driver.find_element(By.XPATH, "(//input[@type='checkbox'])[2]")  # checkbox thứ 2
    checkbox.click()
    time.sleep(5)


    # click button order
    order_button = driver.find_element(By.XPATH, "//button[span[text()='Đặt hàng']]")
    order_button.click()
    time.sleep(5)
 
     # tìm và lấy nhân viên thùy ngân
    comboboxCustomer = driver.find_element(By.XPATH, "//input[@name='CustomerComboboxId']")
    comboboxCustomer.send_keys("Thùy Ngân")
    # Nhấn Enter để chọn
    comboboxCustomer.send_keys(Keys.ENTER)
    time.sleep(5)

     # tìm và lấy khuyến mãi mùa hè
    comboboxPromotion = wait.until(EC.presence_of_element_located((By.XPATH, "//input[@name='PromotionComboboxId']")))
    comboboxPromotion.send_keys("Khuyến Mãi Quốc Khánh")
    comboboxPromotion.send_keys(Keys.ENTER)
    time.sleep(5)
    print(f"Chọn Khuyến Mãi Quốc Khánh.")

    condition = wait.until(EC.presence_of_element_located((By.XPATH, "//input[@name='condition']")))
    #  lấy sô lượng khuyến mãi
    quantity = wait.until(EC.presence_of_element_located((By.XPATH, "//input[@name='quantity']")))
    #  lấy phần trăm khuyến mãi
    discount = wait.until(EC.presence_of_element_located((By.XPATH, "//input[@name='discount']")))
    #  lấy tổng tiền
    sum_element = wait.until(EC.presence_of_element_located((By.XPATH, "//input[@name='sum']")))
     #  lấy số lượng sách
    quantityBookId = wait.until(EC.presence_of_element_located((By.XPATH, "//input[@name='quantityBookId']")))

    priceBook = wait.until(EC.presence_of_element_located((By.XPATH, "//input[@name='priceBook']")))
    
    conditionValue = float(condition.get_attribute("value").replace(",", ""))
    q_promotion = int(quantity.get_attribute("value"))
    q_book = int(quantityBookId.get_attribute("value"))
    price = float(priceBook.get_attribute("value").replace(",", ""))
    discount_str = discount.get_attribute("value")  # Ví dụ: "15%"
    discount_percent = float(discount_str.strip().replace("%", ""))
    total_expected = q_book * price 

    # Lấy tổng tiền thực tế từ giao diện
    sum_actual = float(sum_element.get_attribute("value").replace(",", ""))
    print(f"✅ Phần trăm khuyến mãi: {discount_str}.")
    print(f"✅ Điều kiện khuyến mãi lớn hơn: {conditionValue}.")
    
    print(f"Tiền được tính(có khuyến mãi): {total_expected}.")
    print(f"Tiền thực tế: {sum_actual}.")
    print(f"✅ Điều kiện khuyến mãi: {conditionValue}. > Tiền thực tế: {sum_actual}.")
    # So sánh, cho phép sai số nhỏ do số thực
    if q_promotion > 0:
        if abs(total_expected - sum_actual) < 0.01:
            print(f"✅ Tiền được tính(không khuyến mãi): {total_expected} bằng tiền thực tế: {sum_actual}.")
        else:
            print("❌ Tổng tiền KHÔNG đúng!")
            print(f"👉 Kết quả mong đợi: {total_expected}, Thực tế: {sum_actual}")
    else:
         print("❌ Hết số lượng khuyến mãi!")
    time.sleep(10)


except Exception as e:
    print(f"❌ Đã xảy ra lỗi: {e}")

finally:
    driver.quit()
