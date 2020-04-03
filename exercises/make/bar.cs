using static System.Console;

public class Bar{
	int n;
	public Bar(int bars){
		n = bars;
	}
	public void Foobar(){
		string result = "Foo";
		
		for(int i=0; i<n; i++){
			result += "bar";
		}
		WriteLine(result);
	}
}
