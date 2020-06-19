using System;
using static System.Math;

public class Minimization{
	public static (vector, int) qNewton(
			Func<vector,double> f, // Function phi
			vector x0, // Starting point
			double eps // Desired accuracy
			){
		int steps = 0;
		vector x = x0.copy();
		matrix B = resetB(x.size);
		vector dfdx = gradient(f, x);
		while(dfdx.norm() > eps){
			steps++;
			// Calc dx
			vector dx = -1*B*dfdx;
			// Backtracking search
			double lambda = 1;
			
			matrix dxT = new matrix(1, dx.size);
			for(int i = 0; i < dx.size; i++) dxT[0, i] = dx[i]; 
			
			double armijo = (dxT * dfdx)[0]; // Armijo condition
			while(f(x + lambda * dx) > f(x) + lambda*armijo / 10000){
				if(lambda < 1.0/64){
					B = resetB(x.size);
					break;
				}
				lambda /= 2;
			}
			// SR1 update of B
			vector y = gradient(f, x + lambda*dx) - dfdx;
			vector u = lambda * dx - B*y;
			matrix uT = new matrix(1, u.size);
			for(int i = 0; i < u.size; i++) uT[0, i] = u[i];
			double uTy = (uT * y)[0];
			if(Abs(uTy) > 1e-6) B.update(u,u, 1/uTy);

			// Update x, dfdx
			x = x + lambda * dx;
			dfdx = gradient(f, x);
		}
		return (x, steps);
	}

	private static vector gradient(Func<vector, double> f, vector x, double dx = 1e-7){
		// Calculate vector of f at x
		vector p = x.copy();
		vector grad = new vector(x.size);
		for(int i = 0; i < x.size; i++){
			p[i] = p[i] + dx;
			grad[i] = (f(p) - f(x))/dx;
			p[i] = p[i] - dx;
		}
		return grad;
	}

	private static matrix resetB(int n){
		matrix B = new matrix(n,n);
		for(int i = 0; i < n; i++) B[i,i] = 1;
		return B;
	}
}
