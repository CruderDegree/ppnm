MATHDIR = ../../math/
LIBFILES = $(MATHDIR)ANN.cs $(MATHDIR)Minimization.cs $(MATHDIR)vector.cs $(MATHDIR)matrix.cs

.PHONY : all clean

all: Plot-A.svg

Plot-A.svg: plot-A.gpi data-A.txt
	gnuplot $<

data-A.txt : main.exe
	mono $< > $@

main.exe: main.cs lib.dll
	mcs $< -r:lib.dll

lib.dll : $(LIBFILES)
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.svg *.txt *.exe
