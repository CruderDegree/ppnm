using static System.Console;
using static math;

class errorfunc{
	static int Main(){
		double eps = 1.0/32, dx = 1.0/16;
		for(double x=-3+eps; x<3; x+=dx)
			WriteLine($"{x,10:f3} {math.erf(x),15:f8}");	
		return 0;
	}
}
