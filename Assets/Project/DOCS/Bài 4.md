# Bài 4 #
## I. Bài cũ ##
### 1. Trigger ###
- Trigger trong Unity là một vùng không gian Collider được thiết lập cho phép các đối tượng đi xuyên qua nhau nhưng vẫn phát hiện ra thời điểm tiếp xúc để kích hoạt sự kiện.
- Cách hoạt động cơ bản: 
	+ Không cản trở vật lý: Đối tượng khác sẽ đi xuyên qua Trigger chứ không bị nảy ra hay chặn lại.
	+ Điều kiện: Ít nhất một trong hai đối tượng phải có thành phần Rigidbody (hoặc ArticulationBody) và bật tùy chọn Is Trigger trên Collider.
- Các hàm sự kiện chính (Trigger Methods):
	+ OnTriggerEnter(Collider other): Gọi một lần khi đối tượng bắt đầu đi vào vùng Trigger.
	+ OnTriggerStay(Collider other): Gọi liên tục mỗi khung hình khi đối tượng vẫn đang ở bên trong vùng Trigger.
	+ OnTriggerExit(Collider other): Gọi một lần khi đối tượng đi ra khỏi vùng Trigger.
- Nếu không tích chọn "Is Trigger", các đối tượng sẽ va chạm, nảy ra hoặc chặn nhau lại (như quả bóng đập vào tường). Lúc này sẽ sử dụng bộ hàm OnCollision:
	+ OnCollisionEnter(Collision collision): Gọi một lần ngay khi hai đối tượng bắt đầu chạm vào nhau.
	+ OnCollisionStay(Collision collision): Gọi liên tục mỗi khung hình khi hai đối tượng vẫn đang tiếp xúc.
	+ OnCollisionExit(Collision collision): Gọi một lần ngay khi hai đối tượng tách nhau ra.
- Nếu làm game 2D và sử dụng các thành phần như Rigidbody 2D và Collider 2D thì chỉ cần thêm chữ 2D vào cuối tên các hàm:
	+ Khi bật "Is Trigger" trong 2D: <br>
		OnTriggerEnter2D(Collider2D other) <br>
		OnTriggerStay2D(Collider2D other) <br>
		OnTriggerExit2D(Collider2D other) <br>
	+ Khi va chạm vật lý thông thường trong 2D: <br>
		OnCollisionEnter2D(Collision2D collision) <br>
		OnCollisionStay2D(Collision2D collision) <br>
		OnCollisionExit2D(Collision2D collision) <br>
		
| Đặc điểm | Bộ hàm Trigger | Bộ hàm Collision |
| --- | --- | --- |
| Xuyên qua |Có (Đi xuyên qua nhau)|Không (Bị cản lại/Đẩy ra)|
|Tham số truyền vào| Trả về Collider (Chỉ biết đối tượng đó là ai)|Trả về Collision (Chứa thêm thông tin lực va chạm, điểm tiếp xúc contacts)|
|Hiệu năng|Nhẹ hơn, xử lý nhanh hơn|Nặng hơn do phải tính toán lực vật lý|
### 2. Raycast 2D ###
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
### 3. Layer mask ###
- Layer Mask trong Unity là một bộ lọc dùng để chọn hoặc loại trừ các Layer (lớp đối tượng) cụ thể khi thực hiện các tác vụ như render hình ảnh (Camera Culling Mask) hoặc kiểm tra va chạm (Raycast, Physics).
- Ý nghĩa và Công dụng:
	+ Giới hạn Camera (Culling Mask): Quyết định xem Camera sẽ hiển thị (vẽ) những đối tượng thuộc Layer nào và bỏ qua Layer nào.
	+ Lọc va chạm vật lý (Physics/Raycast): Giúp hàm kiểm tra va chạm chỉ quét các đối tượng ở Layer định sẵn (ví dụ: chỉ va chạm với môi trường, bỏ qua nhân vật) để tối ưu hiệu năng.
	+ Cách hoạt động: Layer Mask hoạt động dưới dạng mặt nạ bit (bitmask) trong lập trình, cho phép gộp nhiều Layer lại với nhau trong một biến duy nhất.
### 4. Prefab ###
- Prefab trong Unity là một dạng tài sản (Asset) giúp lưu trữ một GameObject cùng toàn bộ thành phần (Component) và các đối tượng con của nó để tái sử dụng nhiều lần trong dự án.
- Định nghĩa: Prefab hoạt động như một khuôn mẫu (template).
- Đồng bộ hóa: Khi bạn sửa đổi Prefab gốc, mọi bản sao (instance) trong Scene sẽ tự động cập nhật theo.
- Lợi ích: Tiết kiệm thời gian, quản lý đối tượng dễ dàng và tạo ra các đối tượng khi game đang chạy (ví dụ như đạn bắn hoặc kẻ địch) thông qua code.
### 5. Instantiate ###
- Trong Unity, Instantiate là một hàm dùng để tạo ra một bản sao (clone) của một đối tượng có sẵn (như GameObject hoặc Prefab) khi game đang chạy.
- Mục đích sử dụng:
	+ Spawn nhân vật/vật phẩm: Tạo ra đạn khi bắn súng, sinh ra kẻ địch (enemy) liên tục trên màn hình.
	+ Tạo giao diện động: Hiển thị danh sách item trong kho đồ hoặc thông báo mới.
- Ví dụ:
```
public GameObject bulletPrefab; // Khai báo Prefab viên đạn

// Tạo ra viên đạn tại vị trí của nhân vật (transform.position)
Instantiate(bulletPrefab, transform.position, Quaternion.identity);
```
## II. Bài mới ##
### 1. Action ###
- Action trong Unity C# là một delegate (hàm ủy nhiệm) có sẵn của hệ thống, đại diện cho một hàm không trả về giá trị (kiểu void) và không có tham số truyền vào. Ngoài ra, C# cũng hỗ trợ Action<T> để đại diện cho các hàm có chứa tham số.
- Nói một cách đơn giản, Action giống như một "thùng chứa" lệnh hoặc một hộp thư. Bạn có thể bỏ một hoặc nhiều hàm vào trong đó, sau đó chỉ cần gọi Action một lần duy nhất để kích hoạt tất cả các hàm đã lưu cùng một lúc.
- Ứng dụng:
	+ Giảm sự phụ thuộc (Decoupling): Giúp các script không cần phải biết quá nhiều về nhau. Ví dụ: Khi nhân vật hết máu, Script Health chỉ cần kích hoạt một Action tên là OnPlayerDeath. Các script khác như SoundManager (bật nhạc tử trận) hay UIManager (hiện bảng Game Over) chỉ cần lắng nghe Action đó để tự chạy lệnh của mình.
	+ Viết code gọn gàng hơn: Thay vì phải tự định nghĩa các delegate thủ công một cách dài dòng, bạn có thể dùng ngay Action có sẵn.
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