# Bài 2 #
## I.Tóm tắt bài cũ ##
### 1.Gameobject ###
- GameObject trong Unity là đối tượng cơ bản và quan trọng nhất, đại diện cho mọi thực thể xuất hiện trong màn chơi (Scene), dù bạn có nhìn thấy chúng hay không.
- Đặc điểm chính của GameObject
	+ Thực thể vạn năng: Nó có thể là nhân vật, kẻ địch, viên đạn, cây cối, ánh sáng (Light), âm thanh (Audio) hay cả máy quay (Camera).
	+ Cái khung rỗng: Bản thân một GameObject khi mới tạo ra chỉ là một chiếc khung trống không có hình dáng hay chức năng gì cả.
	+ Chứa Component (Thành phần): Sức mạnh của GameObject đến từ các Component được gắn vào nó.
### 2.Vòng đời của GameObject (The Unity GameObject lifecycle) ###
- là chuỗi các trạng thái và sự kiện mà một đối tượng (GameObject) trải qua từ khi được tạo ra, hoạt động trong scene cho đến khi bị hủy hoặc tắt.
- Bao gồm:
	+ Khởi tạo (Initialization):
		* Awake(): Chạy đầu tiên khi script được tải, ngay cả khi script chưa bật (enabled). Dùng để gán giá trị biến hoặc khởi tạo dữ liệu giữa các script.
		* OnEnable(): Chạy ngay sau Awake (hoặc mỗi khi GameObject/script được bật lại). Dùng để đăng ký sự kiện.
		* Start(): Chạy trước khung hình (frame) đầu tiên, nhưng chỉ khi script được bật. Dùng để lấy dữ liệu sau khi các thành phần khác đã khởi tạo xong qua Awake().
	+ Vòng lặp vật lý (Physics):
		* FixedUpdate(): Chạy theo khoảng thời gian vật lý cố định (không phụ thuộc vào khung hình/FPS). Dùng để xử lý các tính toán liên quan đến Rigidbody và vật lý.
	+ Vòng lặp khung hình (Game Logic / Rendering):
		* Update(): Chạy mỗi khung hình một lần. Dùng để xử lý logic chính như nhận input từ bàn phím, di chuyển đối tượng không dùng vật lý.
		* LateUpdate(): Chạy sau khi tất cả hàm Update() đã hoàn tất. Dùng để cập nhật camera bám theo nhân vật hoặc các xử lý cần tính toán xong vị trí mới của đối tượng khác.
	+ Hủy bỏ và kết thúc (Destruction / Deactivation):
		* OnDisable(): Chạy khi đối tượng hoặc script bị tắt (disabled). Dùng để hủy đăng ký sự kiện.
		* OnDestroy(): Chạy khi GameObject bị hủy hoàn toàn khỏi bộ nhớ. Dùng để dọn dẹp tài nguyên.
### 3.Time ###
- Time.deltaTime: là khoảng thời gian (tính bằng giây) đã trôi qua kể từ khung hình (frame) trước cho đến khung hình hiện tại.
	+ Tác dụng: Giúp tốc độ di chuyển hoặc thay đổi của vật thể không bị phụ thuộc vào cấu hình máy mạnh hay yếu (FPS cao hay thấp).
	+ Ứng dụng: Di chuyển nhân vật (Dùng trong hàm Update() để dịch chuyển nhân vật mượt mà, không bị giật cục khi FPS thay đổi) hay Đếm thời gian/Cooldown (Dùng để đếm ngược thời gian hồi chiêu kỹ năng hoặc thời gian chờ trong game).
- Time.fixedDeltaTime: là khoảng thời gian (tính bằng giây) giữa các lần gọi hàm FixedUpdate() liên tiếp, chuyên dùng để xử lý các tính toán vật lý.
	+ Tác dụng: Time.deltaTime (thay đổi theo số khung hình/FPS của từng máy), Time.fixedDeltaTime giữ nguyên một giá trị không đổi (mặc định là 0.02 giây, tương đương 50 lần cập nhật mỗi giây).
	+ Ứng dụng: Có thể đọc giá trị này hoặc tự gán lại giá trị mới trong code nếu muốn thay đổi tốc độ của hệ thống vật lý (ví dụ: làm hiệu ứng slow-motion cho vật lý).
- Time.unscaledDeltaTime: không bị ảnh hưởng bởi hệ số tốc độ thời gian (Time.timeScale).
### 4.Mathf ###
- Mathf là tập hợp các hàm toán học được tối ưu hóa cho kiểu dữ liệu số thực (float) trong Unity.
- Lí do sử dụng: Unity dùng kiểu float cho hầu hết vị trí, chuyển động và thông số đồ họa. Thư viện System.Math của C# dùng kiểu double, gây lãng phí bộ nhớ và bắt buộc phải ép kiểu liên tục. Mathf cung cấp sẵn nhiều hàm tiện ích chuyên dụng cho lập trình game mà System.Math không có sẵn.
- Hàm phổ biến:
	+ Nhóm nội suy (Interpolation) – Tạo chuyển động mượt mà:
		* Mathf.Lerp(a, b, t): Nội suy tuyến tính giữa a và b dựa trên tỷ lệ t (từ 0 đến 1). Thường dùng để di chuyển mượt mà từ điểm A đến điểm B.
		* Mathf.Clamp(value, min, max): Giới hạn một giá trị không được nhỏ hơn min và không được lớn hơn max. Rất hữu ích để giới hạn máu nhân vật (0 đến 100) hoặc góc quay của camera.
		* Mathf.Clamp01(value): Tiện ích mở rộng của Clamp, tự động giới hạn giá trị trong khoảng từ 0 đến 1.
		* Mathf.MoveTowards(current, target, maxDelta): Dịch chuyển giá trị current về phía target với tốc độ không đổi maxDelta. Hàm này không bị chậm dần như Lerp.
		* Mathf.SmoothStep(from, to, t): Tương tự như Lerp nhưng có hiệu ứng tăng tốc ở đầu và giảm tốc ở cuối, giúp chuyển động tự nhiên hơn.
	+ Nhóm làm tròn và Giá trị tuyệt đối:
		* Mathf.Abs(value): Trả về giá trị tuyệt đối (biến số âm thành số dương). Thường dùng để tính khoảng cách hoặc kiểm tra tốc độ không quan tâm hướng.
		* Mathf.Round(value): Làm tròn đến số nguyên gần nhất.
		* Mathf.Ceil(value): Làm tròn lên số nguyên nhỏ hơn hoặc bằng (ví dụ: 2.1 thành 3).
		* Mathf.Floor(value): Làm tròn xuống số nguyên lớn hơn hoặc bằng (ví dụ: 2.9 thành 2).
	+ Nhóm Lượng giác & Góc (Trigonometry):
		* Mathf.Sin(angle) / Mathf.Cos(angle): Tính Sin và Cos của một góc (lưu ý: góc truyền vào phải tính bằng Radian, không phải độ (Degree)). Thường dùng để làm hiệu ứng bay nhấp nhô hoặc chuyển động tròn.
		* Mathf.Rad2Deg: Hằng số dùng để đổi từ Radian sang Độ (nhân với giá trị Radian).
		* Mathf.Deg2Rad: Hằng số dùng để đổi từ Độ sang Radian (nhân với giá trị Độ).
		* Mathf.DeltaAngle(current, target): Tính khoảng cách ngắn nhất giữa hai góc (tính bằng độ). Rất hữu ích khi xoay camera hoặc xoay nhân vật để không bị lỗi quay ngược vòng lớn.
	+ Nhóm Hàm Lặp (Looping):
		* Mathf.Repeat(t, length): Tạo vòng lặp cho giá trị t sao cho nó không bao giờ vượt quá length (khi đạt đến length, nó sẽ quay về 0). Thường dùng để làm hiệu ứng cuộn nền (scrolling background).
		* Mathf.PingPong(t, length): Di chuyển giá trị qua lại giữa 0 và length giống như quả bóng bàn. Thường dùng để làm đèn nhấp nháy hoặc chướng ngại vật di chuyển qua lại liên tục.
### 5.Gizmos ###
- Gizmos là các công cụ đồ họa trực quan hiển thị trong Scene View để giúp nhà phát triển gỡ lỗi (debug) và thiết lập vị trí đối tượng.
- Đặc điểm: 
	+ Là các hình khối, đường thẳng hoặc biểu tượng (icon) vẽ trực tiếp trong không gian 3D. 
	+ Chỉ hiển thị trong Scene View (và tùy chọn trong Game View nếu bật icon). 
	+ Không xuất hiện trong bản build game chính thức. 
	+ Được quản lý thông qua lớp lập trình Unity Gizmos API.
- Cách sử dụng cơ bản: Sử dụng hàm OnDrawGizmos() hoặc OnDrawGizmosSelected() trong tập lệnh (script) C#:
	```
	private void OnDrawGizmosSelected() {
    	Gizmos.color = Color.red;
    	Gizmos.DrawWireSphere(transform.position, 2.0f);
	}
	```
### 6.Transform ###
- Transform trong Unity là thành phần (component) cơ bản bắt buộc xuất hiện trên mọi đối tượng (GameObject), dùng để lưu trữ và quản lý vị trí, góc xoay và tỉ lệ kích thước của đối tượng đó trong không gian 3D hoặc 2D.
- Các thuộc tính chính của Transform:
	+ Position (Vị trí): Xác định tọa độ (x, y, z) của đối tượng trong thế giới game hoặc so với đối tượng cha.
	+ Rotation (Góc xoay): Xác định hướng và góc quay của đối tượng theo các trục X, Y, Z.
	+ Scale (Tỉ lệ): Phóng to hoặc thu nhỏ kích thước của đối tượng theo ba chiều.
## II.Bài mới ##
### 1.New input system ###
- New Input System trong Unity là một gói (package) quản lý thiết bị đầu vào mới thay thế cho hệ thống cũ (Input Manager). Nó giúp xử lý các thao tác từ bàn phím, chuột, màn hình cảm ứng, tay cầm chơi game (gamepad) và nhiều thiết bị khác một cách linh hoạt và mạnh mẽ hơn.
- Đặc điểm nổi bật:
	+ Hỗ trợ đa nền tảng tốt hơn: Dễ dàng cấu hình chung cho nhiều loại thiết bị mà không cần viết lại mã nguồn xử lý riêng lẻ.
	+ Tách biệt logic và thiết bị: Cho phép ánh xạ (mapping) các phím bấm linh hoạt thông qua bảng điều khiển cấu hình (Input Actions asset).
	+ Hỗ trợ sự kiện (Events) và Polling: Cung cấp cả cách tiếp cận theo hướng sự kiện hiện đại lẫn cách kiểm tra trạng thái phím truyền thống.
### 2.Physic 2D ###
#### 2.1.Va chạm ####
- Va chạm trong Unity là sự kiện xảy ra khi hai đối tượng (GameObject) giao nhau hoặc tiếp xúc trong không gian trò chơi.
- Trong Unity, hệ thống chia va chạm thành hai loại chính dựa trên cách xử lý vật lý:
	+ Collision (Va chạm vật lý cứng): Ngăn cản hai đối tượng xuyên qua nhau và tạo ra lực đẩy vật lý thực tế (như bóng nảy lên, nhân vật chạm tường). Dùng hàm OnCollisionEnter.
	+ Trigger (Va chạm xuyên qua/mềm): Cho phép đối tượng đi xuyên qua nhau nhưng vẫn phát hiện ra thời điểm tiếp xúc (dùng để nhặt vật phẩm, đi qua vùng kích hoạt sự kiện). Dùng hàm OnTriggerEnter.
- Các thành phần quan trọng:
	+ Collider: Định hình vùng không gian va chạm của đối tượng (Box Collider, Sphere Collider, Capsule Collider...).
	+ Rigidbody: Giúp đối tượng chịu ảnh hưởng của trọng lực và các lực vật lý khác.
- Các điều kiện cần và đủ để xảy ra va chạm:
	+ Phải có Collider: Cả hai đối tượng (hoặc ít nhất một đối tượng kết hợp với RigidBody) phải được gắn thành phần Collider (ví dụ: Box Collider, Sphere Collider, Capsule Collider...). Đây là vùng không gian giới hạn hình dạng va chạm của đối tượng.
	+ Phải có Rigidbody (Vật lý chuyển động): Ít nhất một trong hai đối tượng phải gắn thành phần Rigidbody (hoặc Rigidbody2D trong 2D) để chịu ảnh hưởng của trọng lực và lực vật lý. Nếu không có Rigidbody, Unity coi đối tượng là vật tĩnh (Static Collider) và có thể không kích hoạt được một số sự kiện va chạm động.
	+ Kích hoạt Script xử lý: Đoạn mã (Script) bắt sự kiện va chạm (như OnCollisionEnter hoặc OnTriggerEnter) phải được gắn vào đối tượng chứa Collider/Rigidbody đó.
#### 2.2.Rigidbody 2D ####
- Rigidbody 2D trong Unity là một thành phần (component) giúp đưa đối tượng game (GameObject) vào hệ thống mô phỏng vật lý 2 chiều, cho phép vật thể chịu ảnh hưởng của trọng lực, lực đẩy, vận tốc và va chạm.
- Các tính năng: 
	+ Chịu lực vật lý: Giúp đối tượng có thể rơi xuống do trọng lực hoặc di chuyển khi có lực tác động mà không cần viết mã lệnh phức tạp.
	+ Thay thế Transform: Tự động cập nhật vị trí (position) và góc xoay (rotation) của đối tượng dựa trên tính toán vật lý.
	+ Xử lý va chạm: Phối hợp với các thành phần va chạm (Collider 2D) để nhận diện và phản hồi khi chạm vào các vật thể khác.
- Các loại Body Type trong Rigidbody 2D:
	+ Dynamic: Chịu toàn bộ tác động vật lý (trọng lực, lực va chạm, vận tốc). Thường dùng cho nhân vật chính, kẻ địch hoặc các vật thể rơi.
	+ Kinematic: Không chịu tác động của lực hay trọng lực bên ngoài, nhưng có thể di chuyển thông qua mã lệnh (script). Thường dùng cho các bục di chuyển.
	+ Static: Đứng yên hoàn toàn, không di chuyển ngay cả khi có lực tác động. Dùng cho tường hoặc mặt đất cố định.
- Các thuộc tính cơ bản:
	+ Mass: Khối lượng của vật thể, ảnh hưởng đến độ nặng nhẹ khi va chạm.
	+ Linear Drag: Sức cản không khí làm giảm tốc độ di chuyển của vật theo thời gian.
	+ Gravity Scale: Hệ số nhân trọng lực tác động lên đối tượng (có thể chỉnh âm để bay lên hoặc bằng 0 để mất trọng lực).
#### 2.3.Raycast 2D ###
- Raycast 2D trong Unity là một phương thức vật lý dùng để bắn một tia thẳng (ray) từ một điểm trong không gian 2D theo một hướng nhất định nhằm phát hiện các đối tượng có chứa thành phần va chạm (Collider2D).
- Nguyên lý hoạt động:
	+ Điểm xuất phát (Origin): Vị trí bắt đầu bắn tia (ví dụ: vị trí nhân vật).
	+ Hướng (Direction): Hướng mà tia di chuyển tới (ví dụ: hướng nhìn của nhân vật).
	+ Khoảng cách (Distance): Chiều dài tối đa của tia.
	+ Kết quả: Trả về thông tin của đối tượng đầu tiên mà tia cắt qua (RaycastHit2D), giúp biết đối tượng đó là gì, vị trí va chạm ở đâu.
- Ứng dụng phổ biến:
	+ Kiểm tra mặt đất (Grounded Check): Xem nhân vật đang đứng trên mặt đất hay đang nhảy trên không.
	+ Bắn súng / Tấn công: Xác định viên đạn hoặc đòn đánh trúng kẻ địch nào.
	+ Tương tác chuột (Click/Tap): Phát hiện người chơi bấm vào vật phẩm nào trên màn hình 2D.
	+ Tầm nhìn của AI (Line of Sight): Kiểm tra xem kẻ địch có nhìn thấy người chơi hay bị vách ngăn che khuất.
- Ví dụ:
```
// Bắn một tia từ vị trí hiện tại sang bên phải, khoảng cách là 5 đơn vị
RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 5f);

if (hit.collider != null) {
    // Đã va chạm với đối tượng
    Debug.Log("Đã bắn trúng: " + hit.collider.name);
}
```
#### 2.4.Layer mask ####
- Layer Mask trong Unity là một bộ lọc dùng để chọn hoặc loại trừ các Layer (lớp đối tượng) cụ thể khi thực hiện các tác vụ như render hình ảnh (Camera Culling Mask) hoặc kiểm tra va chạm (Raycast, Physics).
- Ý nghĩa và Công dụng:
	+ Giới hạn Camera (Culling Mask): Quyết định xem Camera sẽ hiển thị (vẽ) những đối tượng thuộc Layer nào và bỏ qua Layer nào.
	+ Lọc va chạm vật lý (Physics/Raycast): Giúp hàm kiểm tra va chạm chỉ quét các đối tượng ở Layer định sẵn (ví dụ: chỉ va chạm với môi trường, bỏ qua nhân vật) để tối ưu hiệu năng.
	+ Cách hoạt động: Layer Mask hoạt động dưới dạng mặt nạ bit (bitmask) trong lập trình, cho phép gộp nhiều Layer lại với nhau trong một biến duy nhất.
#### 2.5.Di chuyển nhân vật ###
- Gồm có 3 cách chính:
	+ Thay đổi Transform.position trực tiếp: 
		* Dùng mã nguồn để cộng trực tiếp tọa độ (Vector3) theo thời gian (Time.deltaTime).
		* Thích hợp cho các game đơn giản, di chuyển dạng lưới (Grid-based) hoặc không cần va chạm phức tạp.
	+ Sử dụng Rigidbody (Vật lý):
		* Thêm thành phần Rigidbody (hoặc Rigidbody2D) vào nhân vật.
		* Dùng hàm AddForce hoặc thay đổi vận tốc velocity để nhân vật di chuyển mượt mà theo trọng lực và va chạm thực tế.
	+ Sử dụng Character Controller:
		* Đây là một component dựng sẵn chuyên dụng cho nhân vật dạng góc nhìn thứ nhất hoặc thứ ba.
		* Giúp xử lý va chạm với tường và mặt đất tự động mà không bị trượt hay nảy như Rigidbody.