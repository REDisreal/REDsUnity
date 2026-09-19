# Bài 6 #
### 1. Animator Controller ###
- Animator Controller trong Unity 2D là một tài nguyên (asset) dùng để quản lý và điều khiển các trạng thái hoạt ảnh (animation) cùng các quy tắc chuyển đổi (transition) giữa chúng cho một nhân vật hoặc đối tượng.
- Nó hoạt động dựa trên mô hình State Machine (Máy trạng thái hữu hạn), giúp bạn thiết lập logic hoạt họa bằng giao diện trực quan thay vì phải viết code điều khiển phức tạp.
- Các thành phần chính:
	+ States (Trạng thái): Mỗi ô đại diện cho một Animation Clip (ví dụ: Idle, Run, Jump, Attack).
	+ Transitions (Sự chuyển đổi): Các mũi tên nối giữa các State, quy định khi nào đối tượng chuyển từ hoạt ảnh này sang hoạt ảnh khác.
	+ Parameters (Tham số): Biến số dùng làm điều kiện kích hoạt Transition (gồm 4 loại: Float, Int, Bool, và Trigger).
	+ Any State: Trạng thái đặc biệt cho phép chuyển sang một hoạt ảnh khác từ bất kỳ trạng thái hiện tại nào (thường dùng cho hoạt ảnh bị thương, chết).
- Cách hoạt động cơ bản trong Unity 2D:
	1. Gắn Component: Thêm component Animator vào GameObject 2D và gán file Animator Controller vào ô Controller.
	2. Tái cấu trúc State: Kéo thả các file .anim vào cửa sổ Animator window để tạo các State.
	3. Cấu hình Parameter: Tạo tham số (ví dụ: isWalking kiểu Bool, Speed kiểu Float).
	4. Tạo Condition: Tạo mũi tên nối từ Idle sang Run, cài đặt Condition: isWalking == true.
	5. Cập nhật qua Code: Dùng C# để thay đổi tham số theo sự kiện bàn phím/gameplay.
- Ví dụ code:
```
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        // Chuyển đổi trạng thái di chuyển
        if (inputX != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Kích hoạt animation nhảy
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
        }
    }
}
```
### 2. Animator Component ###
- Animator Component là thành phần (component) trên GameObject trong Unity, đóng vai trò làm "cầu nối" đưa các thiết lập từ Animator Controller vào đối tượng thực tế trong scene.

- Nếu Animator Controller chứa sơ đồ logic và danh sách các trạng thái animation, thì Animator Component chính là bộ máy thực thi logic đó lên đối tượng.
- Các thuộc tính quan trọng (Inspector):

| Thuộc tính | Chức năng |
| --- | --- |
| Controller | Gán file Animator Controller chứa sơ đồ trạng thái hoạt ảnh. |
| Avatar | Gán cấu hình xương (chủ yếu dùng cho mô hình 3D). Đối với 2D, phần này thường để trống (None). |
| Apply Root Motion | Cho phép vị trí/xoay của GameObject bị điều khiển trực tiếp bởi animation thay vì code Transfrom. (Với 2D, thường bỏ chọn để tự xử lý di chuyển bằng Rigidbody2D/Code). |
| Update Mode | Quy định thời điểm cập nhật animation:<br>• Normal: Cập nhật theo Update() (mặc định).<br>• Animate Physics: Cập nhật theo FixedUpdate() (dùng khi animation liên quan đến vật lý/Rigidbody2D).<br>• Unscaled Time: Cập nhật bất chấp game bị Pause (Time.timeScale = 0).|
| Culling Mode | Tối ưu hiệu năng bằng cách dừng animation khi không xuất hiện trên màn hình (Cull Update Transforms hoặc Cull Completely). |
### 3. Animation Clip ###
- Animation Clip (Tệp phân đoạn hoạt ảnh) là tài nguyên cơ bản nhất trong hệ thống hoạt họa của Unity, chứa dữ liệu chuyển động thực tế của một hành động cụ thể (như Idle, Walk, Jump, hay Attack).

- Trong Unity 2D, một Animation Clip lưu trữ chuỗi các hình ảnh (Sprite Keyframes) hoặc các thay đổi thuộc tính của GameObject theo thời gian.
- Các đặc tính chính
	+ Keyframe (Điểm mốc): Lưu lại thông số của đối tượng tại một mốc thời gian cụ thể. Giữa hai Keyframe, Unity sẽ tự động tính toán (interpolate) để chuyển động diễn ra mượt mà.

	+ Property Curve (Đường cong thuộc tính): Quy định sự thay đổi của các thuộc tính theo thời gian (như vị trí Position, góc xoay Rotation, màu sắc Color, hoặc Sprite hiển thị).

	+ Loop Time: Tùy chọn cho phép hoạt ảnh lặp lại liên tục khi chạy hết thời gian (dùng cho các hành động lặp như chạy, đứng yên).

	+ Frame Rate (Sample Rate): Số khung hình trên mỗi giây (thường là 12 hoặc 60 fps tùy theo phong cách game 2D).
- Cách tạo Animation Clip cho 2D trong Unity:
	1. Mở cửa sổ Animation bằng cách chọn Window > Animation > Animation (Ctrl + 6).
	2. Chọn GameObject cần tạo hoạt ảnh trên cửa sổ Hierarchy.
	3. Bấm Create trên cửa sổ Animation để lưu tệp .anim mới.
	4. Kéo thả danh sách các hình ảnh Sprite từ cửa sổ Project vào thanh thời gian (Timeline) của cửa sổ Animation.
- Cách gọi và tương tác với Animation Clip:
	+ Thay vì gọi trực tiếp từ code, Animation Clip thường được đưa vào Animator Controller dưới dạng một State. Tuy nhiên, bạn cũng có thể tương tác với Clip thông qua các tính năng nâng cao:
		* Animation Events: Cho phép gắn hàm C# vào một thời điểm cụ thể trên Timeline của Clip (ví dụ: phát tiếng chân bước đúng thời điểm chân chạm đất trong Clip Walk).
		* Override Animation Clip: Sử dụng AnimatorOverrideController nếu muốn thay đổi tập Clip mới cho nhân vật (ví dụ: đổi skin) mà không cần xây dựng lại toàn bộ sơ đồ trong Animator Controller.
### 4. Blend tree ###
- Blend Tree trong Unity là một dạng trạng thái đặc biệt (State) nằm bên trong Animator Controller, cho phép bạn trộn (blend) nhiều Animation Clip với nhau một cách mượt mà dựa trên các tham số (Parameters) đầu vào, thay vì phải dùng chuyển cảnh dạng bật/tắt cứng ngắc.
- Trong game 2D, Blend Tree thường được ứng dụng phổ biến nhất để xử lý di chuyển theo nhiều hướng (4 hướng, 8 hướng) hoặc chuyển đổi độ mượt giữa Đứng yên - Đi bộ - Chạy.
- Phân loại Blend Tree (2D):
	+ 1D Blending: Trộn hoạt ảnh dựa trên 1 tham số (ví dụ: biến Speed). Dùng để chuyển từ Idle - Walk - Run.
	+ 2D Simple Directional: Trộn dựa trên 2 tham số (ví dụ: MoveX và MoveY) đại diện cho các hướng di chuyển chính (Lên, Down, Left, Right). Mặc định mỗi hướng chỉ nên có 1 clip.
	+ 2D Freeform Directional: Giống 2D Simple nhưng cho phép có nhiều clip trên cùng một hướng (ví dụ: vừa Đi bộ sang trái, vừa Chạy sang trái).
	+ 2D Freeform Cartesian: Dùng cho 2 tham số không đại diện cho hướng (ví dụ: Angular Speed và Linear Speed).
### 5. Finite State Machine ###
- Finite State Machine (FSM) - Máy trạng thái hữu hạn - là một mô hình thiết kế toán học và lập trình dùng để mô tả hành vi của một hệ thống.

- Hệ thống này chỉ có thể tồn tại ở bằng đúng một trạng thái (State) tại một thời điểm nhất định. Khi có các sự kiện hoặc điều kiện kích hoạt (Triggers / Events) thỏa mãn, hệ thống sẽ chuyển đổi (Transition) từ trạng thái hiện tại sang một trạng thái khác.
- Các thành phần cốt lõi của FSM:
	+ State (Trạng thái): Tình trạng hiện tại của hệ thống (ví dụ: Idle, Walking, Jumping, Attacking).

	+ Initial State (Trạng thái ban đầu): Trạng thái mà hệ thống bắt đầu khi được khởi tạo.

	+ Transition (Sự chuyển đổi): Quy tắc quy định việc chuyển từ trạng thái này sang trạng thái khác.

+ Event / Condition (Sự kiện / Điều kiện): Tín hiệu kích hoạt sự chuyển đổi (ví dụ: IsGrounded == false, Press Space Key).
