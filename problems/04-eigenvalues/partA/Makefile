# Makefile of part A
MATHDIR = ../../../math/

all: Answer-A.txt Plot-A.svg

Plot-A.svg: plot-A.gpi data-A.txt
	gnuplot $<

data-A.txt Answer-A.txt: main-A.exe lib-A.dll
	mono $< > Answer-A.txt

main-A.exe: main-A.cs lib-A.dll
	mcs $< -reference:lib-A.dll -out:$@

lib-A.dll: ../lib/vector.cs ../lib/matrix.cs $(MATHDIR)Jacobi.cs $(MATHDIR)MatrixMethods.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.svg *.txt *.exe *.dll
