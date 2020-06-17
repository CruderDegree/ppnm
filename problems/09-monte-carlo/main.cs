using static System.Console;
using static System.Math;
using static MonteCarlo;
using System.IO;
using System;

class main{
	static void Main(){
		WriteLine("---- Part A ----");
		WriteLine("Firstly, we test the plain MC on some integrals:");
		Func<vector, double> f;
		vector a, b;
		int N = 500000;
		double res, err;

		WriteLine("∫_0^a 1/Sqrt(a*a - x*x) dx = π/2 ≈ 1.57079632679 , with a = 10");
		a = new vector(0.0);
		b = new vector(10.0);
		f = (x) => 1/Sqrt(100 - x.dot(x));
		(res, err) = plainMC(f, a, b, N);
		WriteLine($"Result (N = {N}): {res} +- {err}");

		WriteLine("∫_0^π/2 sin^2(x) dx ∫_0^π/2 cos^2(y) dy = π*π/16 ≈ 0.616850275068");
		a = new vector(0.0, 0.0);
		b = new vector(PI/2, PI/2);
		f = (x) => Sin(x[0])*Sin(x[0])*Cos(x[1])*Cos(x[1]);
		(res, err) = plainMC(f, a, b, N);
		WriteLine($"Result (N = {N}): {res} +- {err}");
		WriteLine("");
		WriteLine("Next, we test it on this beast:");
		WriteLine("∫_0^π dx/π ∫_0^π dy/π ∫_0^π dz/π [1-cos(x)cos(y)cos(z)]^-1 = Γ(1/4)^4/(4π^3) ≈ 1.39320392968567");
		a = new vector(0.0, 0.0, 0.0);
		b = new vector(PI, PI, PI);
		f = (x) => 1/(1-Cos(x[0])*Cos(x[1])*Cos(x[2]))/PI/PI/PI;
		(res, err) = plainMC(f, a, b, N);
		WriteLine($"Result (N = {N}): {res} +- {err}");

		WriteLine("\n ---- Part B ----");
		WriteLine("See figure PlotB.svg to see that the error is O(1/sqrt(N))");
		using (StreamWriter sw = new StreamWriter("dataB.txt")){
			int[] Ns = new int[] {1000, 2500, 5000, 7500, 10000, 
				15000, 20000, 25000, 50000, 75000, 100000, 125000,
				150000, 175000, 200000, 250000, 300000, 400000, 500000};
			foreach(int n in Ns){
				(res, err) = plainMC(f,a,b,n);
				sw.WriteLine($"{n} {err}");
			}
		}
	}
}
