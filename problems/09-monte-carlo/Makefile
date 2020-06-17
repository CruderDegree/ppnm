LIBFILES = lib/vector.cs lib/MonteCarlo.cs

.PHONY : all clean test

all: Answer.txt PlotB.svg

PlotB.svg: plotB.gpi dataB.txt
	gnuplot $<

Answer.txt dataB.txt: main.exe
	mono $< > Answer.txt

main.exe: main.cs lib.dll
	mcs $< -r:lib.dll -out:$@

lib.dll: $(LIBFILES)
	mcs $^ -target:library -out:$@

clean: 
	rm -f *.dll *.txt *.exe *.svg

