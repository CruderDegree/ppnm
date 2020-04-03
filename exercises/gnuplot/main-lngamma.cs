using static System.Console;
using static math;

class lngammafunc{
	static int Main(){
		double eps = 1.0/32, dx = 1.0/32;
		for(double x=0+eps; x<6; x+=dx)
			WriteLine($"{x,10:f3} {math.lngamma(x),15:f8}");	
		return 0;
	}
}
