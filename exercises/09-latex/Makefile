E=2.718281828
LIBDIR=../func

all: report.pdf

report.pdf: report.tex plot.tex
	pdflatex $<
	pdflatex $<

plot.tex: plot.gpi out-table.txt out-data.txt
	gnuplot $<

out-table.txt: Makefile
	cat /dev/null > $@
	echo "$$(echo "1.0/$E/$E" | bc -l) -2" >> $@
	echo "$$(echo "1.0/$E" | bc -l) -1" >> $@
	echo "$$(echo "sqrt($E)" | bc -l) 0.5" >> $@
	echo '1 0' >> $@
	echo '$E 1' >> $@
	echo "$$(echo "$E * $E" | bc -l) 2" >> $@
	#echo "$$(echo "$E * $E * $E" | bc) 3" >> $@
	#echo "$$(echo "$E * $E * $E * $E" | bc) 4" >> $@
	#echo "$$(echo "$E * $E * $E * $E * $E" | bc) 5" >> $@
	#echo "$$(echo "$E * $E * $E * $E * $E * $E" | bc) 6" >> $@
	#echo "$$(echo "$E * $E * $E * $E * $E * $E * $E" | bc) 7" >> $@

out-data.txt: main.exe
	mono $< > $@ 2> out-error.txt

main.exe: main.cs mathlib.dll
	mcs $< -reference:mathlib.dll

mathlib.dll: $(LIBDIR)/quado8.cs
	mcs $< -target:library -out:$@

.PHONEY: clean
clean: 
	rm -f *.dll *.exe *.log *.aux *.pdf plot.tex out-error.txt *.eps out-data.txt
