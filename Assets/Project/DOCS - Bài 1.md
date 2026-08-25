# Bài 1 #
## I. Tóm tắt bài cũ ##
### 1. Tham chiếu, tham trị ###
- Truyền tham trị là truyền giá trị của biến, có nghĩa là nó sẽ tạo ra một ô nhớ mới để lưu trữ và chỉ cho phép biến thay đổi bên trong phương thức hiện hành. 
- Truyền tham chiếu là truyền địa chỉ ô nhớ của biến, do đó khi thay đổi giá trị của biến bên trong phương thức thì giá trị của biến cũng bị thay đổi bên ngoài phương thức.
- Ví dụ truyền tham chiếu:<br>
	
	int x = 10; <br>
	int y = x; // Sao chép giá trị 10 sang y <br>
	y = 20;    // Thay đổi y <br>
	
	Console.WriteLine(x); // Kết quả: 10 (x không bị thay đổi) <br>
	Console.WriteLine(y); // Kết quả: 20 <br>
- Ví dụ truyền tham trị: <br>
	
	public class Person 
	{
		public string Name;
	}

	Person p1 = new Person() { Name = "An" }; <br>
	Person p2 = p1; // p2 trỏ cùng địa chỉ ô nhớ trên Heap với p1

	p2.Name = "Bình"; // Thay đổi qua p2

	Console.WriteLine(p1.Name); // Kết quả: Bình (p1 bị ảnh hưởng) <br>
	Console.WriteLine(p2.Name); // Kết quả: Bình
### 2. Class ###
- Trong C#, class là một khuôn mẫu (blueprint) hoặc một kiểu dữ liệu do người dùng tự định nghĩa. Nó bao gồm các thuộc tính để lưu trữ dữ liệu và các phương thức để định nghĩa hành vi của đối tượng.
- Ví dụ:
	
	```
	using System;

	namespace Example{
	
    // Định nghĩa class
    public class Person{
    
        // 1. Trường dữ liệu (Fields)
        private string name;

        // 2. Thuộc tính (Properties)
        public int Age { get; set; }

        // 3. Phương thức khởi tạo (Constructor)
        public Person(string name, int age)
        {
            this.name = name;
            this.Age = age;
        }

        // 4. Phương thức (Methods)
        public void SayHello()
        {
            Console.WriteLine($"Xin chào, tôi là {name}, {Age} tuổi.");
        }
    }

    class Program{
    
        static void Main(string[] args){
            	// Khởi tạo đối tượng từ class
            	Person person1 = new Person("An", 25);
            
            	// Gọi phương thức
            	person1.SayHello(); // Output: Xin chào, tôi là An, 25 tuổi.
        	}
    	}
 	}
## II. Kiến thức mới ##
### 1. MonoBehaviour ###
- MonoBehaviour là lớp cơ sở (base class) trong Unity mà hầu hết các script C# đều kế thừa từ đó. Nó cho phép script gắn trực tiếp vào một GameObject và truy cập vào vòng đời (Lifecycle) cũng như các sự kiện của Unity.

- Nếu không kế thừa MonoBehaviour, file C# của bạn chỉ là một lớp thông thường (C# pure class) và không thể kéo thả vào các đối tượng trên màn hình (Inspector/Scene).

- Các đặc tính chính của MonoBehaviour

	+ Gắn vào GameObject: Cho phép script trở thành một Component của đối tượng trong game.

	+ Tự động gọi các hàm vòng đời (Lifecycle Methods): Unity tự động quản lý và gọi các hàm sự kiện theo thứ tự nhất định mà bạn không cần khởi tạo đối tượng bằng từ khóa new.

	+ Cung cấp biến và phương thức tích hợp: Giúp truy cập nhanh đến các component khác trên cùng GameObject như transform, gameObject, GetComponent<T>(), hay các lệnh như StartCoroutine(), Destroy().
- Các hàm vòng đời (Event Functions) phổ biến:
```
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 1. Chạy ĐẦU TIÊN khi script được load (kể cả khi script chưa bật/enable)
    void Awake()
    {
        Debug.Log("Awake: Khởi tạo biến hoặc tham chiếu.");
    }

    // 2. Chạy ngay trước frame đầu tiên (chỉ chạy khi script được bật)
    void Start()
    {
        Debug.Log("Start: Thiết lập các thông số ban đầu.");
    }

    // 3. Chạy MỖI FRAME (dùng cho logic game, nhận input từ người chơi)
    void Update()
    {
        // Ví dụ: Bấm space để nhảy
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump!");
        }
    }

    // 4. Chạy theo khoảng thời gian cố định (dùng cho tính toán Vật lý - Physics/Rigidbody)
    void FixedUpdate()
    {
        // Di chuyển bằng lực vật lý ở đây
    }

    // 5. Chạy khi 2 đối tượng va chạm nhau (yêu cầu có Collider)
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Đã va chạm với: " + collision.gameObject.name);
    }
}
```