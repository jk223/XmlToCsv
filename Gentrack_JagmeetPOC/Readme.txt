Assumptions
1) It is assumed that lines in CSVIntervalData are separated by newline. More work need to be done if this not true
2) Logic can be optimized if the input file is very large, it can be read into multiple splits.
3) Implemented thread safe reading and writing in case multiple instances of this application runs.
4) If the send element [which is file name] is repeated in any row, it will override existing file. This case can be handled after discussion what need to be done in such situation.
5) Unit test covers the validation part mostly and it does not cover the validation of generated csv file. More time is required to implement it.
6) Proper patterns are used to scale or enhance this solution later. Algoriths can further be optimized.
7) Some comments being written to provide clarity, but proper documentation and proper names of variable, class etc can be further optimized.
