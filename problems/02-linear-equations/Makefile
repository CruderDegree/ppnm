all: Answer.txt

Answer.txt: main.exe
	mono $< > $@

main.exe: main.cs matrixlib.dll
	mcs $< -reference:matrixlib.dll

matrixlib.dll: lib/matrix.cs lib/vector.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.exe *.dll *.txt
