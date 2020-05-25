using static System.Console;

class epsilon{
	static void Main(){
		WriteLine("Max int");
		int i = 100000000; while(i+1 > i){i++;}
		WriteLine($"Found max value: {i}");
		WriteLine($"Actual max value: {int.MaxValue}");
		WriteLine("");
		WriteLine("Min int");
		i = -100000000; while(i-1 < i){i--;}
		WriteLine($"Found min value: {i}");
		WriteLine($"Actual min value: {int.MinValue}");
		WriteLine("");
		WriteLine("Machine epsilon");
		double x=1; while(1+x!=1){x/=2;} x*=2;
		float y=1F; while( (float)(1F+y) != 1F){y/=2F;} y*=2F;
		WriteLine($"Found double machine epsilon: {x}");
		WriteLine($"Actual double machine epsilon: {System.Math.Pow(2,-52)}");
		WriteLine("");	
		WriteLine($"Found float machine epsilon: {y}");
		WriteLine($"Actual float machine epsilon: {System.Math.Pow(2,-23)}");
	}
}
