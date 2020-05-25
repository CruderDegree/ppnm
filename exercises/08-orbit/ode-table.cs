using System;
using static System.Math;
using static System.Console;

class main{
	static int Main(){
		Func<double,double> f = (x) => 1/(1+Exp(-x));		
		for (double i = 0; i<4; i+=1.0/32){
			WriteLine($"{i} {f(i)}");
		}
		return 0;
	}
}

		
