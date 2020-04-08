using System;
using static System.Console;

public class qspline{
	double[] x, y, b, c;

	public qspline(double[] x, double[] y){
		// Evaluate coefficients b and c of qspline
		this.x = x;
		this.y = y;
		this.b = new double[x.Length - 1];
		this.c = new double[x.Length - 1];
		
		// calculate c's from bottom and top and average
		for (int i = 0; i < this.c.Length - 3; i++){
			this.c[i+1] = 1/(x[i+2] - x[i+1]) * ((y[i+2] - y[i+1])/(x[i+2]-x[i+1])
					- (y[i+1] - y[i])/(x[i+1] - x[i]) -
					this.c[i]*(x[i+1]-x[i]));
		}
		this.c[this.c.Length - 1] /= 2;
		for (int i = this.c.Length - 2; i >= 0; i--){
			this.c[i] = 1/(x[i+1]-x[i]) * ((y[i+2]-y[i+1])/(x[i+2]-x[i+1]) -
					(y[i+1]-y[i])/(x[i+1]-x[i]) -
					this.c[i+1]*(x[i+2]-x[i+1]));
		}
		// average c's and calc b's
		for (int i = 0; i < x.Length - 1; i++){
			this.b[i] = (y[i+1] - y[i])/(x[i+1] - x[i]) - this.c[i]*(x[i+1] - x[i]);
		}	
	}

	public double spline(double z){
		// Evaluate spline at z
		int i = binsearch(z);
		return this.y[i] + this.b[i]*(z-this.x[i]) + 
			this.c[i]*(z-this.x[i])*(z-this.x[i]);
	}

	public double derivative(double z){
		// eval deriv of qspline at z
		int i = binsearch(z);
		return this.b[i] + 2*this.c[i]*(z-this.x[i]);
	}

	public double integral(double z){
		// calc int from x[0] to z of spline
		double res  = 0;
		int i = 0;
		// Integrate between x[i] and x[i+1] while z>x[i+1]
		while(this.x[i + 1] < z){
			res += 	this.y[i]*(this.x[i+1] - this.x[i]);
			res += 	1.0/2*this.b[i]*
				(this.x[i] - this.x[i+1] )*(this.x[i] - this.x[i+1]);
			res -=	1.0/3*this.c[i]*(this.x[i] - this.x[i+1])
				*(this.x[i]-this.x[i+1])*(this.x[i] - x[i+1]);
			i++;
		}
		// integrate from x[i] to z	
		res += 	this.y[i]*(z - this.x[i]);
		res += 	1.0/2*this.b[i]*(this.x[i] - z)*(this.x[i] - z);
		res -=	1.0/3*this.c[i]*(this.x[i] - z)*(this.x[i] - z)*(this.x[i] - z);	
		return res;
	}

	int binsearch(double z){
		int i = 0, j = this.x.Length - 1;
		while(j - i > 1){
			int mid = (i+j)/2;
			if(z>this.x[mid]) i = mid; else j = mid;
		}
		return i;
	}
}
