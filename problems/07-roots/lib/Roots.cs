using System;
using static System.Math;
using static LinEqs;

public class Roots{
	public static vector newton(Func<vector, vector> f, vector p,
			double eps = 1e-3, double dx = 1e-7){
		vector x = p.copy();
		// Find a root of 'f', starting at point x
		matrix J = new matrix(x.size, x.size);
		while(f(x).norm() > eps){
			// Generate J
			makeJacobian(f, x, J, dx);
			// Solve J dx = -f(x) for dx
			vector deltax = qr_givens_solve(J, -1*f(x));
			double lambda = 1;
			while(Sqrt(f(x + lambda * deltax).dot(f(x + lambda * deltax))) > (1-lambda/2)*f(x).norm() && lambda > 1.0/64){
				lambda /= 2;
			}
			for(int i = 0; i < x.size; i++) x[i] = x[i] + lambda*deltax[i];		
		}
		return x;
	}

	private static void makeJacobian(Func<vector, vector> f, vector x, matrix J, double dx){
		// Calculates Jacobian matrix
		vector xk = x.copy();
		for(int i = 0; i < x.size; i++) for(int k = 0; k < x.size; k++){
			xk[k] = xk[k] + dx;
			J[i,k] = (f(xk)[i] - f(x)[i])/dx;
			xk[k] = xk[k] - dx;
		}
	}
}
