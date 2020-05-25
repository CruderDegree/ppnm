using static System.Console;
using static math;
using static cmath;

class complexgammafunc{
	static int Main(){
		double eps = 1.0/32, da = 1.0/16, db = 1.0/16;
		for(double a=-4+eps; a<4; a+=da){
			for(double b=-4+eps;b < 4; b+=db){
				complex z = new complex(a,b);
				WriteLine($"{a,10:f3} {b,10:f3} {cmath.abs(math.cgamma(z)),15:f8}");
			}
		}
		return 0;
	}
}
