# Benchmarks of the different samples over time
We let each problem run a maximum of 10 minutes  
A running time "-" denotes an execution that did not complete in the allotted time

# scope-c
| Commit | Running&nbsp;Time | Moves | Cost | Paths&nbsp;Explored | Solution |
|--------|-------------------|-------|------|---------------------|----------|
|(a220fb0...)|0s|8|16|344|AG,1,2;C,1,0;AGD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
| | | | | |

# speed-c
| Commit | Running&nbsp;Time | Moves | Cost | Paths&nbsp;Explored | Solution |
|--------|-------------------|-------|------|---------------------|----------|
|(a220fb0...)|0s|10|21|4128|AG,1,2;C,1,0;AGD,2,1;CA,2,1;C,1,0;BC,3,2;B,2,1;DC,3,2;D,2,1;CCC,2,0|
| | | | | |

# easy-a
| Commit | Running&nbsp;Time | Moves | Cost | Paths&nbsp;Explored | Solution |
|--------|-------------------|-------|------|---------------------|----------|
|(a220fb0...)|7m17s|14|34|96.508.301|ACC,2,1;AA,5,2;AAA,2,0;CDG,3,2;ADG,3,2;A,3,2;AA,2,1;AAA,1,0;DGD,4,3;DG,5,4;DGG,4,3;A,6,5;AA,5,4;AAA,4,0|
| | | | | |

# hard-d
| Commit | Running&nbsp;Time | Moves | Cost | Paths&nbsp;Explored | Solution |
|--------|-------------------|-------|------|---------------------|----------|
|(a220fb0...)|-|31|83|103.682.137|D,1,0;G,1,2;C,1,5;D,1,0;GAC,2,1;CAC,2,1;DG,4,2;CD,3,2;GA,3,2;D,4,3;GAD,4,3;GAC,2,4;GA,3,4;DDD,3,0;GAD,3,2;GA,2,3;DDD,2,0;GCG,2,3;D,2,0;CAA,5,2;DGA,5,2;DCG,5,3;D,3,2;D,5,2;DDD,2,0;ACG,6,5;DCG,6,5;D,6,5;EGD,6,5;EG,5,6;DDD,5,0|
| | | | | |
