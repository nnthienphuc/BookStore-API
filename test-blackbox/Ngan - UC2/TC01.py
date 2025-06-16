from selenium import webdriver
from selenium.webdriver.edge.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.keys import Keys
import time

# Đường dẫn Edge WebDriver
EDGE_DRIVER_PATH = "C:\\DaiCa\\Project\\BookStore\\Test\\SeleniumTest\\msedgedriver.exe"

# Khởi động WebDriver
service = Service(EDGE_DRIVER_PATH)
driver = webdriver.Edge(service=service)
try:
    # vào trang đăng nhập
    driver.get("https://localhost:7226/")
    time.sleep(3)

    # Điền thông tin đăng nhập
    driver.find_element(By.ID, "email").send_keys("phucnaoto@gmail.com")
    driver.find_element(By.ID, "password").send_keys("12345678")
    time.sleep(3)

    # click btn login
    driver.find_element(By.XPATH, "//button[text()='Sign In']").click()
    time.sleep(10)
    print("Bỏ qua chọn đơn hàng.")
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
    # click button save
    order_button = driver.find_element(By.XPATH, "//button[span[text()='Save']]")
    order_button.click()

    # Đợi toast chứa text xuất hiện trong vòng 5 giây
    WebDriverWait(driver, 5).until(
        EC.text_to_be_present_in_element(
            (By.CLASS_NAME, "custom-toast-background"),
            "Đơn hàng phải có ít nhất một sản phẩm."
        )
    )
    #nếu k chạy cái đợi toast thì dòng này k đc chạy
    print("❌ Tạo thất bại đơn hàng.")
    time.sleep(10)

except Exception as e:
    print(f"❌ Đã xảy ra lỗi: {e}")

finally:
    driver.quit()
