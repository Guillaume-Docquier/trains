# Benchmarks of the different samples over time
A running time 10m+ denotes an execution that did not complete after 10 minutes.  
Note that the number of paths explored are not exact figures since the execution speed depends on what ran on the CPU at the time.  
They do, however, give good indications on execution speed.  

# Notable commits
(a220fb0...) If a better solution exists, it must exist in less or equal moves  
(9a9cb95...) Take distance into consideration for the potential evaluation  
(d36e848...) Potential Evaluation Optimization  

# scope-c
| Commit | Running&nbsp;Time | Moves | Cost | Paths&nbsp;Explored | Solution |
|--------|-------------------|-------|------|--------------------:|----------|
|**(a220fb0...)**|0s|8|16|344|AG,1,2;C,1,0;AGD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
|(05d9692...)|0s|8|16|779|AG,1,2;C,1,0;AGD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
|(a85beac...)|0s|8|16|1407|AG,1,2;C,1,0;AGD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
|**(9a9cb95...)**|0s|8|16|2357|AG,1,2;C,1,0;AGD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
|**(d36e848...)**|2s|8|16|627.540|AG,1,2;C,1,0;AGD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
|(dfe3107...)|2m5s|8|16|54.877.166|AG,1,2;C,1,0;AGD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
|(f7964d9...)||||||
|(53deb39...)||||||
|(4e38515...)||||||
|||||||

# speed-c
| Commit | Running&nbsp;Time | Moves | Cost | Paths&nbsp;Explored | Solution |
|--------|-------------------|-------|------|--------------------:|----------|
|**(a220fb0...)**|0s|10|21|4128|AG,1,2;C,1,0;AGD,2,1;CA,2,1;C,1,0;BC,3,2;B,2,1;DC,3,2;D,2,1;CCC,2,0|
|(05d9692...)|0s|10|21|26115|AG,1,2;C,1,0;AGD,2,1;CA,2,1;C,1,0;BC,3,2;B,2,1;DC,3,2;D,2,1;CCC,2,0|
|(a85beac...)|0s|10|21|39066|AG,1,2;C,1,0;AGD,2,1;CA,2,1;C,1,0;BC,3,2;B,2,1;DC,3,2;D,2,1;CCC,2,0|
|**(9a9cb95...)**|0s|10|21|98300|AG,1,2;C,1,0;AGD,2,1;CA,2,1;C,1,0;BC,3,2;B,2,1;DC,3,2;D,2,1;CCC,2,0|
|**(d36e848...)**|1m2s|10|21|19.987.886|AG,1,2;C,1,0;AGD,2,1;CA,2,1;C,1,0;BC,3,2;B,2,1;DC,3,2;D,2,1;CCC,2,0|
|(dfe3107...)|10m+|12|26|248.232.075|A,1,2;G,1,2;C,1,0;A,1,3;GAD,2,1;CA,2,1;C,1,0;ABC,3,2;AB,2,1;DC,3,2;D,2,1;CCC,2,0|
|(f7964d9...)||||||
|(53deb39...)||||||
|(4e38515...)||||||

# easy-a
| Commit | Running&nbsp;Time | Moves | Cost | Paths&nbsp;Explored | Solution |
|--------|-------------------|-------|------|--------------------:|----------|
|**(a220fb0...)**|7m17s|14|34|96.508.301|ACC,2,1;AA,5,2;AAA,2,0;CDG,3,2;ADG,3,2;A,3,2;AA,2,1;AAA,1,0;DGD,4,3;DG,5,4;DGG,4,3;A,6,5;AA,5,4;AAA,4,0|
|(05d9692...)|10m+|17|47|116.958.668|A,2,0;C,2,1;C,2,1;A,2,0;C,3,1;D,3,1;G,3,1;A,3,0;DG,3,2;AA,5,3;AAA,3,0;DGD,4,3;DG,5,4;DGG,4,3;A,6,5;AA,5,4;AAA,4,0|
|(a85beac...)|10m+|||||
|**(9a9cb95...)**|10m+|||||
|**(d36e848...)**|10m+|||||
|(dfe3107...)|10m+|||||
|(f7964d9...)|10m+|||||
|(53deb39...)|10m+|||||
|(4e38515...)|10m+|||||

# hard-d
| Commit | Running&nbsp;Time | Moves | Cost | Paths&nbsp;Explored | Solution |
|--------|-------------------|-------|------|--------------------:|----------|
|**(a220fb0...)**|10m+|31|83|103.682.137|D,1,0;G,1,2;C,1,5;D,1,0;GAC,2,1;CAC,2,1;DG,4,2;CD,3,2;GA,3,2;D,4,3;GAD,4,3;GAC,2,4;GA,3,4;DDD,3,0;GAD,3,2;GA,2,3;DDD,2,0;GCG,2,3;D,2,0;CAA,5,2;DGA,5,2;DCG,5,3;D,3,2;D,5,2;DDD,2,0;ACG,6,5;DCG,6,5;D,6,5;EGD,6,5;EG,5,6;DDD,5,0|
|(05d9692...)|10m+|-|-|74.156.831|-|
|(a85beac...)|10m+|||||
|**(9a9cb95...)**|10m+|||||
|**(d36e848...)**|10m+|||||
|(dfe3107...)|10m+|||||
|(f7964d9...)|10m+|||||
|(53deb39...)|10m+|||||
|(4e38515...)|10m+|||||
