LIBFILES = lib/*

all: Plot.svg Answer.txt

Plot.svg: plot.gpi data/output.txt
	gnuplot $<

data/output.txt Answer.txt: main.exe
	mono $< 2> data/output.txt > Answer.txt

main.exe: main.cs mathlib.dll
	mcs $< -reference:mathlib.dll

mathlib.dll: $(LIBFILES)
	mcs $^ -target:library -out:$@

clean:
	rm -f *.exe *.dll data/out.txt Answer.txt *.svg
