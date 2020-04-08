LIN=LinSplinePlot1.svg LinSplinePlot2.svg LinSplinePlot3.svg
QUAD= QuadSplinePlot1.svg QuadSplinePlot2.svg QuadSplinePlot3.svg

all: $(LIN) $(QUAD)

$(LIN): linSpline1.gpi linSpline2.gpi linSpline3.gpi
	gnuplot $^

linSpline1.gpi: data-lin-1.txt tab-lin-1.txt Makefile
	cat /dev/null > $@ 
	echo 'set term svg background rgb "white"' >> $@
	echo 'set out "LinSplinePlot1.svg"' >> $@
	echo 'set title "Linear Interpolation of f(x)=x"' >> $@
	echo 'set key bottom right' >> $@
	echo 'set xrange[1:5]' >> $@ 
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "f(x)"' >> $@
	echo 'plot [1:5][] x t "f(x) = x" ,\
	"$<" using 1:2 with point pt 7 ps 0.5 title "Interpolated points" ,\
	"$<" u 1:3 with point pt 7 ps 0.5 title "Interpolated integral" ,\
	"tab-lin-1.txt" u 1:2 with point pt 2 title "Tabulated Integral values"' >> $@

linSpline2.gpi: data-lin-2.txt Makefile
	cat /dev/null > $@ 
	echo 'set term svg background rgb "white"' >> $@
	echo 'set out "LinSplinePlot2.svg"' >> $@
	echo 'set title "Linear Interpolation of f(x)=x"' >> $@
	echo 'set key bottom right' >> $@
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "f(x)"' >> $@
	echo 'plot[1:5][] x**2 t "f(x) = x^2" ,\
	"$<" with point pt 7 ps 0.5 title "Interpolated points" ,\
	"$<" u 1:3 with point pt 7 ps 0.5 title "Interpolated integral" ,\
	"tab-lin-2.txt" u 1:2 with point pt 2 title "Tabulated Integral values"' >> $@

linSpline3.gpi: data-lin-3.txt Makefile
	cat /dev/null > $@ 
	echo 'set term svg background rgb "white"' >> $@
	echo 'set out "LinSplinePlot3.svg"' >> $@
	echo 'set title "Linear Interpolation of f(x)=sin(x)"' >> $@
	echo 'set key top right' >> $@
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "f(x)"' >> $@
	echo 'plot[0:2*3.1415][] sin(x) t "f(x) = x" ,\
	"$<" with point pt 7 ps 0.5 title "Interpolated points" ,\
	"$<" u 1:3 with point pt 7 ps 0.5 title "Interpolated integral" ,\
	"tab-lin-3.txt" u 1:2 with point pt 2 title "Tabulated Integral values"' >> $@

data-lin-1.txt: main.exe
	mono $< lin 1 > $@ 2> error-lin-1.txt

data-lin-2.txt: main.exe
	mono $< lin 2 > $@ 2> error-lin-2.txt

data-lin-3.txt: main.exe
	mono $< lin 3 > $@ 2> error-lin-3.txt

main.exe: main.cs splinelib.dll 
	mcs $< -reference:splinelib.dll

splinelib.dll: quadspline.cs
	mcs $< -target:library -out:$@

$(QUAD): quadSpline1.gpi quadSpline2.gpi quadSpline3.gpi
	gnuplot $^

quadSpline1.gpi: data-quad-1.txt tab-quad-1.txt Makefile
	cat /dev/null > $@
	echo 'set term svg background rgb "white"' >> $@
	echo 'set out "QuadSplinePlot1.svg"' >> $@
	echo 'set title "Quadritic Spline plot 1"' >> $@
	echo 'set key top left' >> $@
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "y"' >> $@
	echo 'plot "$<" with point pt 7 ps 0.5 title "Interpolated points" ,\
	"tab-quad-1.txt" with point pt 2 ps 1 title "Tabulated points" ,\
	"$<" u 1:3 with point pt 7 ps 0.5 title "Interpolated derivative" ,\
	"$<" u 1:4 with point pt 7 ps 0.5 title "Interpolated integral"' >> $@

quadSpline2.gpi: data-quad-2.txt tab-quad-2.txt Makefile
	cat /dev/null > $@
	echo 'set term svg background rgb "white"' >> $@
	echo 'set out "QuadSplinePlot2.svg"' >> $@
	echo 'set title "Quadritic Spline plot 2"' >> $@
	echo 'set key top left' >> $@
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "y"' >> $@
	echo 'plot "$<" with point pt 7 ps 0.5 title "Interpolated points" ,\
	"tab-quad-2.txt" with point pt 2 ps 1 title "Tabulated points" ,\
	"$<" u 1:3 with point pt 7 ps 0.5 title "Interpolated derivative" ,\
	"$<" u 1:4 with point pt 7 ps 0.5 title "Interpolated integral"' >> $@

quadSpline3.gpi: data-quad-3.txt tab-quad-3.txt Makefile
	cat /dev/null > $@
	echo 'set term svg background rgb "white"' >> $@
	echo 'set out "QuadSplinePlot3.svg"' >> $@
	echo 'set title "Quadritic Spline plot 3"' >> $@
	echo 'set key top left' >> $@
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "y"' >> $@
	echo 'plot "$<" with point pt 7 ps 0.5 title "Interpolated points" ,\
	"tab-quad-3.txt" with point pt 2 ps 1 title "Tabulated points" ,\
	"$<" u 1:3 with point pt 7 ps 0.5 title "Interpolated derivative" ,\
	"$<" u 1:4 with point pt 7 ps 0.5 title "Interpolated integral"' >> $@

data-quad-1.txt: main.exe
	mono $< quad 1 > $@ 2> error-quad-1.txt

data-quad-2.txt: main.exe
	mono $< quad 2 > $@ 2> error-quad-2.txt

data-quad-3.txt: main.exe
	mono $< quad 3 > $@ 2> error-quad-3.txt

clean:
	rm -f *.dll *.exe *.svg data-*.txt error-*.txt *.gpi

