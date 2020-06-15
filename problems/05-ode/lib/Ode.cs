using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;


public class Ode{
	public static void rkStep12(
			Func<double, vector, vector> f, // RHS of dy/dt = f(t, y)
			double t, // Current value of variable
			vector yt, // Current value of y(t)
			double h, // Step size
			vector yh, // output: New value of y(t) after step size h
			vector err // output: Error estimate after step
			){
		/* Performs an embedded Runge-Kutta step using the 
		 * Heun-Euler method.
		 */
		
		// Generate k from Heun's method
		vector k0 = f(t,yt);
		vector k1 = f(t + h, yt + h * k0);
		vector k = 0.5 * (k0 + k1);

		// Calculate new y and error (in-place)
		for(int i = 0; i < yt.size; i++){
			yh[i] = yt[i] + h * k[i];
			// Calculate Error (eq. 23)
			err[i] = k1[i] - k0[i];
			err[i] *= 0.5*h;
		}
	}

	public class AdaptOdeSolver{
		/* Adaptive step size ODE solver
		 */ 
		// --- Fields
		private Func<double, vector, vector> f;
		private double a, b; // Start and end points
		private double h, absAcc, relAcc; // 
		private vector y;
		private List<vector> ys;
		private List<double> xs;
		
		// --- Properties
		public List<vector> yData{
			get {return ys;}
		}
		public List<double> xData{
			get {return xs;}
		}

		// --- Constructor
		public AdaptOdeSolver(
				Func<double, vector, vector> f, 
				double a,
				vector y,
				double b,
				double h,
				double absAcc,
				double relAcc
				){
			this.f = f;
			this.a = a;
			this.b = b;
			this.h = h;
			this.absAcc = absAcc;
			this.relAcc = relAcc;
			this.y = y;
			
			this.ys = new List<vector> {y.copy()};
			this.xs = new List<double> {a};

			drive();
		}

		// Driver
		private void drive(){
			// Setup first step
			int k = 0, n = y.size;
			double x, s, err, normy, tol;
			vector yh = new vector(n);
			vector dy = new vector(n);

			// Drive
			while(xs[k] < b){
				x = xs[k]; y = ys[k];
				if(x+h > b) h = b-x;
				// Take step
				rkStep12(f,x,y,h,yh,dy);
				// Calculate error
				s = 0; for(int i = 0; i<n; i++) s+=dy[i]*dy[i];
				err = Sqrt(s);
				s = 0; for(int i = 0; i<n; i++) s+=yh[i]*yh[i];
				normy = Sqrt(s);
				tol = (normy * relAcc + absAcc) * Sqrt(h / (b-a));
				Write($"x: {x} \n");
				y.print("y: ");
				yh.print("yh: ");
				// Check if step is good enough to continue
				if(err < tol){
					k++;
					xs.Add(x+h);
					ys.Add(yh.copy());
				}
				// Update Step size
				if(err > 0) h*=Pow(tol/err, 0.25) * 0.95;
				else h*= 2;
			}
		}
	}

}
