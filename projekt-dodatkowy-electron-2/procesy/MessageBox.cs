
namespace a;

public class MessageBox {
	public static void Show(string msg) {
		Con.connection.Send("message_box", msg);
	}
}