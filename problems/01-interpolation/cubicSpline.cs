using static System.Console; 
using System.Collections.Generic; // For List object

class main{
	static int Main(string[] Args){
		// Read data from input file
		string s;
		List<double> x = new List<double>();
		List<double> y = new List<double>();
		do{
			s = ReadLine();
			if(s != null){
				string[] words = s.Split('\t');
				x.Add(double.Parse(words[0]));
				y.Add(double.Parse(words[1]));
			}
		}while(s != null);
		// Make cubic spline
		CubicSpline Spline = new CubicSpline(x, y);
		// Write data
		double eps = 1.0/8;
		for(double z = x[0]; z<=x[x.Count-1]; z+=eps){
			WriteLine(
			$"{z} {Spline.spline(z)} {Spline.derivative(z)} {Spline.integral(z)}");
		}
		return 0;
	}
}

public class CubicSpline{
	// Fields
	List<double> x,y;
	double[] b,c,d;
	// Constructor
	public CubicSpline(List<double> x, List<double> y){
		int n = x.Count;
		this.x = x; this.y = y;
		this.b = new double[n];
		this.c = new double[n-1];
		this.d = new double[n-1];

		// Aux. constants
		double[] h = new double[n-1];
		double[] p = new double[n-1];
		for(int i=0; i < n-1; i++){
			h[i] = x[i+1] - x[i];
			p[i] = (y[i+1] - y[i])/h[i];
		}

		// Build tridiagonal system
		double[] D = new double[n];
		D[0] = 2; D[n-1] = 2;
		for(int i=0; i<n-2; i++){
			D[i+1] = 2*h[i]/h[i+1] + 2;
		}

		double[] Q = new double[n-1];
		Q[0] = 1;
		for(int i=0; i<n-2; i++){
			Q[i+1] = h[i]/h[i+1];
		}

		double[] B = new double[n];
		B[0] = 3*p[0]; B[n-1] = 3*p[n-2];
		for(int i=0; i<n-2; i++){
			B[i+1] = 3*(p[i] + p[i+1]*h[i]/h[i+1]);
		}

		// Gauss elimination
		double[] Dt = new double[n];
		double[] Bt = new double[n];
		Dt[0] = D[0]; Bt[0] = B[0];
		for(int i = 1; i<n; i++){
			Dt[i] = D[i] - Q[i-1]/Dt[i-1];
			Bt[i] = B[i] - Bt[i-1]/Dt[i-1];
		}

		// Back-substitution
		this.b[n-1] = Bt[n-1]/Dt[n-1];
		for(int i = n-2; i>=0; i--) this.b[i] = (Bt[i] - Q[i]*this.b[i+1])/Dt[i];

		// Use solution for b to calculate c and d
		for(int i=0; i<n-1; i++){
			this.c[i] = (-2*this.b[i] - this.b[i+1] + 3*p[i])/h[i];
			this.d[i] = (this.b[i] + this.b[i+1] - 2*p[i])/(h[i]*h[i]);
		}
	}
	// Evaluate spline
	public double spline(double z){
		int i = binsearch(z);
		double dx = z - x[i];
		return  y[i] + b[i]*dx + c[i]*dx*dx + d[i]*dx*dx*dx;
	}
	// Derivative of spline
	public double derivative(double z){
		int i = binsearch(z);
		double dx = z - x[i];
		return b[i] + 2*c[i]*dx + 3*d[i]*dx*dx;
	}
	
	// Integral of spline
	public double integral(double z){
		// calc int from x[0] to z of spline
		double res  = 0;
		int i = 0;
		double dx;
		// Integrate between x[i] and x[i+1] while z>x[i+1]
		while(this.x[i + 1] < z){
			dx = x[i+1] - x[i];
			res +=
			y[i]*dx+1.0/2*b[i]*dx*dx+1.0/3*c[i]*dx*dx*dx+1.0/4*d[i]*dx*dx*dx*dx;
			i++;
		}
		// integrate from x[i] to z	
		dx = z - x[i];
		res += y[i]*dx + 1.0/2*b[i]*dx*dx + 1.0/3*c[i]*dx*dx*dx +1.0/4*d[i]*dx*dx*dx*dx;
		return res;
	}
	
	// Binary index search
	int binsearch(double z){
		int i = 0, j = this.x.Count - 1;
		while(j - i > 1){
			int mid = (i+j)/2;
			if(z>this.x[mid]) i = mid; else j = mid;
		}
		return i;
	}
}
