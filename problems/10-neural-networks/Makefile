MATHDIR = ../../math/
LIBFILES = $(MATHDIR)ANN.cs $(MATHDIR)Minimization.cs $(MATHDIR)vector.cs $(MATHDIR)matrix.cs

.PHONY : all clean

all: Plot-AB.svg

Plot-AB.svg: plot-AB.gpi data-AB.txt
	gnuplot $<

data-AB.txt : main.exe
	mono $< > $@

main.exe: main.cs lib.dll
	mcs $< -r:lib.dll

lib.dll : $(LIBFILES)
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.svg *.txt *.exe
