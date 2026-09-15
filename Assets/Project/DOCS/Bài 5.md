# Bài 5 #
## I. Bài cũ ##
### 1. Action ###
- Action trong Unity C# là một delegate (hàm ủy nhiệm) có sẵn của hệ thống, đại diện cho một hàm không trả về giá trị (kiểu void) và không có tham số truyền vào. Ngoài ra, C# cũng hỗ trợ Action<T> để đại diện cho các hàm có chứa tham số.
- Nói một cách đơn giản, Action giống như một "thùng chứa" lệnh hoặc một hộp thư. Bạn có thể bỏ một hoặc nhiều hàm vào trong đó, sau đó chỉ cần gọi Action một lần duy nhất để kích hoạt tất cả các hàm đã lưu cùng một lúc.
- Ứng dụng:
	+ Giảm sự phụ thuộc (Decoupling): Giúp các script không cần phải biết quá nhiều về nhau. Ví dụ: Khi nhân vật hết máu, Script Health chỉ cần kích hoạt một Action tên là OnPlayerDeath. Các script khác như SoundManager (bật nhạc tử trận) hay UIManager (hiện bảng Game Over) chỉ cần lắng nghe Action đó để tự chạy lệnh của mình.
	+ Viết code gọn gàng hơn: Thay vì phải tự định nghĩa các delegate thủ công một cách dài dòng thì	 có thể dùng ngay Action có sẵn.
- Ví dụ 1:
```
using System; // Bắt buộc phải có để dùng Action
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // 1. Khai báo một Action
    public Action OnGameStart;

    void Start()
    {
        // 2. Đăng ký các hàm vào Action (nạp lệnh vào thùng)
        OnGameStart += PlayIntroSound;
        OnGameStart += ShowTutorialUI;

        // 3. Kích hoạt Action (Thực thi tất cả các hàm cùng lúc)
        // Dấu ?. dùng để kiểm tra nếu Action có hàm đăng ký thì mới chạy, tránh lỗi Crash game
        OnGameStart?.Invoke(); 
    }

    void PlayIntroSound() => Debug.Log("Đang phát nhạc nền...");
    void ShowTutorialUI() => Debug.Log("Đang hiện hướng dẫn chơi...");
}
```
- Ví dụ 2:
```
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Khai báo Action nhận vào một biến kiểu int (số lượng máu bị mất)
    public Action<int> OnTakeDamage;

    void Start()
    {
        // Đăng ký hàm nhận sát thương
        OnTakeDamage += LogDamage;
        
        // Kích hoạt và truyền giá trị 20 vào hàm
        OnTakeDamage?.Invoke(20);
    }

    void LogDamage(int damageAmount)
    {
        Debug.Log("Người chơi bị mất số máu là: " + damageAmount);
    }
}
```

### 2. Delegate ###
- Trong Unity, delegate (đại biểu) là một kiểu dữ liệu trong C# dùng để tham chiếu đến các phương thức (hàm) có cùng chữ ký (signature), giúp truyền hàm như một tham số hoặc gọi nhiều hàm cùng lúc.
- Tác dụng:
	+ Giao tiếp giữa các script: Giúp các script gọi lẫn nhau mà không cần giữ tham chiếu trực tiếp, giảm sự phụ thuộc (loosely coupled).
	+ Làm callback function: Cho phép một hàm gọi lại một hàm khác sau khi hoàn thành một nhiệm vụ nào đó (ví dụ: tải xong dữ liệu, nhân vật bị mất máu).
	+ Lập trình sự kiện (Event): Kết hợp với từ khóa event để tạo hệ thống thông báo, nơi nhiều đối tượng có thể cùng "lắng nghe" và phản hồi khi có một sự kiện xảy ra.
- Ví dụ:
```
// Khai báo delegate
public delegate void OnHealthChanged(int newHealth);

// Sử dụng delegate trong một class
public event OnHealthChanged onHealthChangedEvent;
```
### 3. Unity Event ###
- Unity Event trong engine làm game Unity là một lớp cho phép bạn gửi và xử lý sự kiện trực quan ngay trong giao diện Inspector mà không cần viết quá nhiều mã nguồn.
- Khái niệm:
	+ Cơ chế hoạt động: Hoạt động như một danh sách chờ (observer pattern), cho phép một thành phần (gửi sự kiện) gọi hàm của nhiều thành phần khác (lắng nghe sự kiện) khi có hành động xảy ra.
	+ Điểm khác biệt: Khác với sự kiện thuần của C# (C# event), UnityEvent có thể tuần tự hóa (serializable) để hiển thị trực tiếp các ô gán hàm (Drag-and-Drop) trong Unity Inspector.
- Ví dụ:
+ Bước 1: Khai báo UnityEvent trong Script.
```
using UnityEngine;
using UnityEngine.Events; // 1. Bắt buộc phải có thư viện này

public class Trap : MonoBehaviour
{
    // 2. Khai báo UnityEvent công khai (Public) để hiển thị lên Inspector
    public UnityEvent onTrapTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 3. Kích hoạt sự kiện khi người chơi chạm vào bẫy
            onTrapTriggered?.Invoke();
        }
    }
}
```

+ Bước 2: Thiết lập phản hồi trong Unity Inspector (Không cần code)<br>Sau khi lưu script trên và gắn vào một Object (ví dụ: quả mìn), bạn sẽ thấy một ô On Trap Triggered () xuất hiện ở bảng Inspector:
	1. Nhấp vào dấu + ở góc dưới cùng bên phải của ô sự kiện.
	2. Kéo Object chứa hàm bạn muốn chạy vào ô None (Object). (Ví dụ: Kéo nhân vật Player hoặc hệ thống âm thanh SoundManager vào).
	3. Nhấp vào ô thả xuống No Function, chọn Component tương ứng và chọn hàm bạn muốn kích hoạt. (Ví dụ: Chọn PlayerHealth -> TakeDamage hoặc AudioSource -> Play).
+ Bước 3: (Tùy chọn) Đăng ký sự kiện bằng Code<br>Nếu bạn không muốn kéo thả bằng tay trong Inspector, bạn có thể đăng ký (lắng nghe) sự kiện đó hoàn toàn bằng mã nguồn:
```
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Trap dangerousTrap; // Gắn script Trap vào đây

    private void OnEnable()
    {
        // Đăng ký hàm xử lý khi bẫy kích hoạt
        dangerousTrap.onTrapTriggered.AddListener(GameOver);
    }

    private void OnDisable()
    {
        // Hủy đăng ký khi không dùng nữa để tránh lỗi bộ nhớ
        dangerousTrap.onTrapTriggered.RemoveListener(GameOver);
    }

    void GameOver()
    {
        Debug.Log("Player dẫm phải bẫy! Game Over!");
    }
}
```
#### 3.1. Các cách đăng kí sự kiện của unity ####
1. Sử dụng C# Events & Delegates (Khuyến khích dùng cho code)
- Bước 1: Tạo Script phát sự kiện (Publisher): Tạo một script để định nghĩa và kích hoạt sự kiện khi có điều gì đó xảy ra (ví dụ: Người chơi bị dính sát thương).
```
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // 1. Khai báo sự kiện sử dụng Action (một dạng delegate có sẵn của C#)
    public static event Action OnPlayerDeath;

    public void Die()
    {
        Debug.Log("Người chơi đã chết!");
        
        // 2. Kích hoạt sự kiện (Kiểm tra xem có ai đăng ký không trước khi gọi)
        OnPlayerDeath?.Invoke();
    }
}
```
- Bước 2: Tạo Script lắng nghe và đăng ký sự kiện (Subscriber): Tạo một script khác (ví dụ: GameManager hoặc UIManager) để lắng nghe sự kiện trên và xử lý.
```
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Bắt buộc phải đăng ký khi Object được bật lên
    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += GameOverScreen;
    }

    // Bắt buộc phải hủy đăng ký khi Object bị tắt/hủy để tránh rò rỉ bộ nhớ (Memory Leak)
    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= GameOverScreen;
    }

    // Hàm sẽ tự động chạy khi sự kiện OnPlayerDeath được kích hoạt
    private void GameOverScreen()
    {
        Debug.Log("Hiện bảng Game Over lên màn hình!");
    }
}
```
2. Sử dụng UnityEvent (Trực quan trên Inspector)
- Script:
```
using UnityEngine;
using UnityEngine.Events; // Cần thêm thư viện này

public class Chest : MonoBehaviour
{
    // Khai báo một UnityEvent công khai
    public UnityEvent OnChestOpened;

    public void OpenChest()
    {
        Debug.Log("Rương đã mở!");
        // Kích hoạt sự kiện
        OnChestOpened?.Invoke();
    }
}
```
- Cách đăng ký trên giao diện Unity:
	+ Chọn GameObject chứa script Chest.
	+ Trên bảng Inspector, bạn sẽ thấy mục On Chest Opened xuất hiện một ô trống với dấu +.
	+ Nhấn dấu +, kéo GameObject bạn muốn nhận sự kiện vào ô đó, rồi chọn hàm cần chạy từ menu thả xuống.
3. Đăng ký sự kiện UI (Button, Toggle, Slider...) bằng Code
- Nếu không muốn kéo thả thủ công trên giao diện UI, bạn hoàn toàn có thể đăng ký sự kiện nhấn nút (OnClick) ngay trong code.
```
using UnityEngine;
using UnityEngine.UI; // Cần thư viện UI

public class MainMenu : MonoBehaviour
{
    public Button startButton;

    private void Start()
    {
        // Đăng ký sự kiện Click cho nút bấm bằng Listener
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        Debug.Log("Bắt đầu vào Game!");
    }

    private void OnDestroy()
    {
        // Xóa listener khi script bị hủy
        startButton.onClick.RemoveListener(OnStartButtonClicked);
    }
}
```

### 4. Coroutine ###
- Coroutine trong Unity là một hàm có khả năng tạm dừng việc thực thi (pause), trả quyền điều khiển về cho Unity, rồi sau đó tiếp tục chạy từ vị trí đã dừng ở các khung hình (frame) tiếp theo.
- Đặc điểm chính của Coroutine:
	+ Tạm dừng linh hoạt: Dùng lệnh yield return để chờ đợi một khoảng thời gian hoặc một điều kiện cụ thể (ví dụ: WaitForSeconds, WaitUntil).
	+ Không làm đơ game: Giúp phân tán công việc nặng hoặc thời gian chờ qua nhiều frame thay vì dồn vào một frame gây giật lag (lag/stutter).
	+ Cú pháp đơn giản: Viết code bất đồng bộ (như độ trễ, đợi hiệu ứng) nhìn giống như code đồng bộ thông thường.
- Ví dụ: Tạo độ trễ cho súng để tăng độ lực cho phát bắn.
```
private IEnumerator ShootRoutine()
    {
        if (m_BulletPrefab == null || m_FirePoint == null) yield break;

        // Cập nhật hồi chiêu và số đạn ngay lập tức
        m_NextFireTime = Time.time + m_FireRate + m_ShootDelay;
        m_CurrentAmmo--;
        OnAmmoChanged?.Invoke(m_CurrentAmmo, m_MaxAmmo);

        // 1. Phát âm thanh NGAY LẬP TỨC khi bấm nút (không delay)
        if (m_AudioSource != null && m_ShootSound != null)
        {
            m_AudioSource.PlayOneShot(m_ShootSound);
        }

        // 2. Tạm dừng 0.5s trước khi viên đạn thực sự xuất hiện
        yield return new WaitForSeconds(m_ShootDelay);

        // 3. Khởi tạo viên đạn tại vị trí FirePoint
        GameObject bulletObj = Instantiate(m_BulletPrefab, m_FirePoint.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        Vector2 shootDirection = m_IsFacingRight ? Vector2.right : Vector2.left;

        if (bullet != null)
        {
            bullet.SetDirection(shootDirection);
        }

        // Tác dụng lực giật lùi cho nhân vật khi đạn được bắn ra
        m_Rigidbody.AddForce(-shootDirection * m_RecoilForce, ForceMode2D.Impulse);
    }
```
- Các lệnh yield return phổ biến:
	+ yield return null;: Tạm dừng và chờ đến frame tiếp theo mới chạy tiếp.
	+ yield return new WaitForSeconds(thời_gian);: Tạm dừng và chờ một khoảng thời gian (tính bằng giây).
	+ yield return new WaitForEndOfFrame();: Chờ cho đến khi tất cả các camera và UI đã render xong frame hiện tại.
	+ yield return new WaitUntil(() => điều_kiện);: Chờ cho đến khi điều kiện (kiểu bool) thỏa mãn.
### 5. Generics ###
- Hàm tự định nghĩa sử dụng Generics trong C# (Generic Method) cho phép bạn viết một phương thức duy nhất có thể hoạt động với nhiều kiểu dữ liệu khác nhau mà không cần viết lại mã.
- Để tạo một hàm generic thêm cặp dấu ngoặc nhọn góc <T> (với T là tham số kiểu dữ liệu) ngay sau tên hàm.
```
public static void Print<T>(T value)
{
    Console.WriteLine(value);
}
```
- Lợi ích của việc sử dụng Genetics:
	+ Tái sử dụng mã: Viết một lần, dùng cho mọi kiểu dữ liệu.
	+ An toàn kiểu dữ liệu (Type-safe): Tránh việc phải ép kiểu từ object và phát hiện lỗi ngay lúc biên dịch (compile-time).
	+ Hiệu suất cao: Không tốn chi phí boxing/unboxing với các kiểu dữ liệu giá trị (value types).
## II. Bài mới ##
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