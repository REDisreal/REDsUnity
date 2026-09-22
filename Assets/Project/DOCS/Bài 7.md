# Bài 7 #
### 1. State machine ###
- State machine (máy trạng thái), hay còn gọi là Finite State Machine (FSM), là một mô hình toán học và lập trình biểu diễn một hệ thống chỉ ở một trạng thái cụ thể trong một tập hợp các trạng thái hữu hạn tại bất kỳ thời điểm nào.
- Các thành phần chính:
	+ Trạng thái (State): Điều kiện hoặc tình huống hiện tại của hệ thống (ví dụ: Đèn đỏ, Đèn vàng, Đèn xanh).
	+ Trạng thái ban đầu (Initial State): Trạng thái bắt đầu của hệ thống khi vừa được kích hoạt.
	+ Sự kiện / Đầu vào (Event / Input): Tác nhân khiến hệ thống thay đổi (ví dụ: Hết giờ, Nhấn nút).
	+ Chuyển đổi (Transition): Hành động dịch chuyển từ trạng thái này sang trạng thái khác dựa trên sự kiện.
### 2. 4 nguyên lý cốt lõi của lập trình hướng đối tượng ###
- 4 nguyên lý cốt lõi của lập trình hướng đối tượng (OOP) bao gồm tính đóng gói, tính kế thừa, tính đa hình và tính trừu tượng.
	+ Tính đóng gói (Encapsulation):
		* Khái niệm: Tính đóng gói là cơ chế che giấu dữ liệu và các chi tiết xử lý bên trong của một đối tượng.
		* Cách thực hiện trong C#: Sử dụng các từ khóa kiểm soát truy cập (Access Modifiers) như private, protected, public, kết hợp với các property (get/set) để kiểm soát việc đọc/ghi dữ liệu.
		* Lợi ích: Bảo vệ dữ liệu không bị thay đổi ngoài ý muốn từ bên ngoài và giúp code dễ bảo trì.
	+ Tính kế thừa (Inheritance):
		* Khái niệm: Cho phép một lớp mới (lớp con/derived class) tái sử dụng và mở rộng các thuộc tính, phương thức từ một lớp đã có (lớp cha/base class) mà không cần viết lại từ đầu.
		* Cách thực hiện trong C#: Dùng dấu hai chấm : để kế thừa (Ví dụ: class Dog : Animal). C# không hỗ trợ đa kế thừa đối với class mà chỉ hỗ trợ kế thừa đơn class, nhưng một class có thể thực thi nhiều interface.
		* Lợi ích: Tiết kiệm thời gian viết code, giảm sự trùng lặp và dễ quản lý cấu trúc chương trình.
	+ Tính đa hình (Polymorphism):
		* Khái niệm: Cho phép các đối tượng thuộc các lớp khác nhau phản hồi cùng một thông điệp hoặc phương thức theo những cách riêng biệt.
		* Cách thực hiện trong C#: Overriding (Ghi đè): Dùng từ khóa virtual ở lớp cha và override ở lớp con để thay đổi nội dung phương thức. / Overloading (Nạp chồng): Viết nhiều phương thức trùng tên nhưng khác nhau về tham số truyền vào trong cùng một class.
		* Lợi ích: Tăng tính linh hoạt cho mã nguồn và giúp viết các hàm xử lý chung cho nhiều đối tượng khác nhau.
	+ Tính trừu tượng (Abstraction):
		* Khái niệm: Là quá trình ẩn đi các chi tiết triển khai phức tạp bên trong và chỉ hiển thị những tính năng, hành động cần thiết ra bên ngoài.
		* Cách thực hiện trong C#: Sử dụng Abstract Class (lớp trừu tượng) hoặc Interface (giao diện).
		* Lợi ích: Giúp giảm độ phức tạp của hệ thống, định hình rõ khung thiết kế trước khi đi vào lập trình chi tiết.
### 3. Class và Object ###
- Class (Lớp) là bản thiết kế hoặc khuôn mẫu, còn Object (Đối tượng) là thể hiện thực tế được tạo ra từ bản thiết kế đó trong C#.
---
- Định nghĩa: Class là một kiểu dữ liệu do người dùng tự định nghĩa, chứa các thuộc tính (dữ liệu/biến) và phương thức (hành vi/hàm) chung cho một nhóm đối tượng.
- Ví dụ thực tế: Giống như bản thiết kế một ngôi nhà. Bản thiết kế quy định nhà có mấy phòng, màu sơn gì, nhưng chưa phải là ngôi nhà thật để ở.
---
- Định nghĩa: Object là một thực thể cụ thể (instance) được khởi tạo từ Class.
- Ví dụ thực tế: Dựa vào bản thiết kế ngôi nhà, ta xây ra ngôi nhà số 1, ngôi nhà số 2. Các ngôi nhà này là "object" cụ thể.
---
|  | Class (Lớp) | Object (Đối tượng) |
| --- | --- | --- | 
| Bản chất | Là bản thiết kế, khuôn mẫu trừu tượng. | Là thể hiện cụ thể (instance) của class. | 
| Cấp phát bộ nhớ | Không chiếm bộ nhớ RAM khi chưa được khởi tạo. | Chiếm bộ nhớ RAM khi được tạo (new). |
| Số lượng | Chỉ khai báo 1 lần trong code. | Có thể tạo ra hàng loạt đối tượng từ 1 class. | 