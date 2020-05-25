out.txt : read-cmdline.exe input.txt Makefile read-stdin.exe
	mono read-cmdline.exe $$(cat input.txt) > out.txt
	mono read-stdin.exe < input.txt >> out.txt

read-cmdline.exe : read-cmdline.cs
	mcs read-cmdline.cs

read-stdin.exe: read-stdin.cs
	mcs read-stdin.cs

.PHONEY:clean
clean:
	rm --force *.exe out* 
