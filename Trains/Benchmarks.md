﻿# Benchmarks of the different samples over time
The benchmark uses 4 problems that are increasingly hard to solve.  
First, an heuristics is ran against the problem to find an upper bound.  
Then, we run a search algorithm.  

The runnning time only mesures the search algorithm.  
A running time '00:10:00+' denotes an execution that did not complete after 10 minutes.  
A number of moves or cost '-' indicates that no better solution was found with the search algorithm.  

# The cost function
The cost function of a move for all the tests is: `1 + Abs(from - to)`

# level-0-c
| Commit | Running&nbsp;Time | Moves | Cost | Paths&nbsp;Explored | Solution |
|--------|-------------------|-------|------|--------------------:|----------|
|[a220fb0](https://github.com/Guillaume-Docquier/trains/commit/a220fb096bfd808ac96dbf479baa462d01d06d54)|00:00:00|8|16|344|AG,1,2;C,1,0;AGD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
|[05d9692](https://github.com/Guillaume-Docquier/trains/commit/05d96929057da4e9431f275ded44df76b4a069ad)|00:00:00|8|16|779|AG,1,2;C,1,0;AGD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
|[a85beac](https://github.com/Guillaume-Docquier/trains/commit/a85beacb22799438f6aa498255c8482bbe330e46)|00:00:00|8|16|1.407|AG,1,2;C,1,0;AGD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
|[9a9cb95](https://github.com/Guillaume-Docquier/trains/commit/9a9cb95d49e3c02ff6ec0aa410686f16d2d2f370)|00:00:00|8|16|2.357|AG,1,2;C,1,0;AGD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
|[d36e848](https://github.com/Guillaume-Docquier/trains/commit/d36e84858c07b6bfa1f6ba574604eedd40b3052c)|00:00:02|8|16|627.540|AG,1,2;C,1,0;AGD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
|[dfe3107](https://github.com/Guillaume-Docquier/trains/commit/dfe31079048f41b877707492fcf33d504c494529)|00:02:05|8|16|54.877.166|AG,1,2;C,1,0;AGD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
|[f7964d9](https://github.com/Guillaume-Docquier/trains/commit/f7964d9a1f19dc05d1c3c6cfd8a96703614ba310)|00:02:27|8|16|54.877.166|AG,1,2;C,1,0;AGD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
|[53deb39](https://github.com/Guillaume-Docquier/trains/commit/53deb39f9433c2bf6a989d448342b66c939b90d1)|00:04:02|8|16|54.877.166|AG,1,2;C,1,0;AGD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
|[4e38515](https://github.com/Guillaume-Docquier/trains/commit/4e38515c1960cdc8c52431ede1f4d8cbfe9f161d)|00:10:00+|9|18|138.201.274|A,1,2;G,1,2;C,1,0;GAD,2,1;CA,2,1;DC,3,2;D,2,3;CC,2,1;CCC,1,0|
|Heuristic|00:00:00|8|21|-|D,3,1;C,3,0;D,2,3;C,2,0;A,2,3;C,2,0;DAG,1,2;C,1,0|

# level-1-c
| Commit | Running&nbsp;Time | Moves | Cost | Paths&nbsp;Explored | Solution |
|--------|-------------------|-------|------|--------------------:|----------|
|[a220fb0](https://github.com/Guillaume-Docquier/trains/commit/a220fb096bfd808ac96dbf479baa462d01d06d54)|00:00:00|10|21|4.128|AG,1,2;C,1,0;AGD,2,1;CA,2,1;C,1,0;BC,3,2;B,2,1;DC,3,2;D,2,1;CCC,2,0|
|[05d9692](https://github.com/Guillaume-Docquier/trains/commit/05d96929057da4e9431f275ded44df76b4a069ad)|00:00:00|10|21|26.115|AG,1,2;C,1,0;AGD,2,1;CA,2,1;C,1,0;BC,3,2;B,2,1;DC,3,2;D,2,1;CCC,2,0|
|[a85beac](https://github.com/Guillaume-Docquier/trains/commit/a85beacb22799438f6aa498255c8482bbe330e46)|00:00:00|10|21|39.066|AG,1,2;C,1,0;AGD,2,1;CA,2,1;C,1,0;BC,3,2;B,2,1;DC,3,2;D,2,1;CCC,2,0|
|[9a9cb95](https://github.com/Guillaume-Docquier/trains/commit/9a9cb95d49e3c02ff6ec0aa410686f16d2d2f370)|00:00:00|10|21|98.300|AG,1,2;C,1,0;AGD,2,1;CA,2,1;C,1,0;BC,3,2;B,2,1;DC,3,2;D,2,1;CCC,2,0|
|[d36e848](https://github.com/Guillaume-Docquier/trains/commit/d36e84858c07b6bfa1f6ba574604eedd40b3052c)|00:01:02|10|21|19.987.886|AG,1,2;C,1,0;AGD,2,1;CA,2,1;C,1,0;BC,3,2;B,2,1;DC,3,2;D,2,1;CCC,2,0|
|[dfe3107](https://github.com/Guillaume-Docquier/trains/commit/dfe31079048f41b877707492fcf33d504c494529)|00:10:00+|12|26|248.232.075|A,1,2;G,1,2;C,1,0;A,1,3;GAD,2,1;CA,2,1;C,1,0;ABC,3,2;AB,2,1;DC,3,2;D,2,1;CCC,2,0|
|[f7964d9](https://github.com/Guillaume-Docquier/trains/commit/f7964d9a1f19dc05d1c3c6cfd8a96703614ba310)|00:10:00+|12|26|216.409.319|A,1,2;G,1,2;C,1,0;A,1,3;GAD,2,1;CA,2,1;C,1,0;ABC,3,2;AB,2,1;DC,3,2;D,2,1;CCC,2,0|
|[53deb39](https://github.com/Guillaume-Docquier/trains/commit/53deb39f9433c2bf6a989d448342b66c939b90d1)|00:10:00+|-|-|103.556.631|-|
|[4e38515](https://github.com/Guillaume-Docquier/trains/commit/4e38515c1960cdc8c52431ede1f4d8cbfe9f161d)|00:10:00+|-|-|119.418.160|-|
|Heuristic|00:00:00|10|28|-|AG,1,3;C,1,0;D,2,1;C,2,0;A,2,1;C,2,0;AGB,3,2;C,3,0;D,3,1;C,3,0|

# level-2-a
| Commit | Running&nbsp;Time | Moves | Cost | Paths&nbsp;Explored | Solution |
|--------|-------------------|-------|------|--------------------:|----------|
|[a220fb0](https://github.com/Guillaume-Docquier/trains/commit/a220fb096bfd808ac96dbf479baa462d01d06d54)|00:07:17|14|34|96.508.301|ACC,2,1;AA,5,2;AAA,2,0;CDG,3,2;ADG,3,2;A,3,2;AA,2,1;AAA,1,0;DGD,4,3;DG,5,4;DGG,4,3;A,6,5;AA,5,4;AAA,4,0|
|[05d9692](https://github.com/Guillaume-Docquier/trains/commit/05d96929057da4e9431f275ded44df76b4a069ad)|08:00:00+|15|39|4.814.040.088|A,2,0;C,2,1;C,2,1;A,2,0;CDG,3,2;ADG,3,1;A,1,0;AA,5,3;AAA,3,0;DGD,4,3;DG,5,4;DGG,4,3;A,6,5;AA,5,4;AAA,4,0|
|[a85beac](https://github.com/Guillaume-Docquier/trains/commit/a85beacb22799438f6aa498255c8482bbe330e46)|-|-|-|-|-|
|[9a9cb95](https://github.com/Guillaume-Docquier/trains/commit/9a9cb95d49e3c02ff6ec0aa410686f16d2d2f370)|-|-|-|-|-|
|[d36e848](https://github.com/Guillaume-Docquier/trains/commit/d36e84858c07b6bfa1f6ba574604eedd40b3052c)|-|-|-|-|-|
|[dfe3107](https://github.com/Guillaume-Docquier/trains/commit/dfe31079048f41b877707492fcf33d504c494529)|-|-|-|-|-|
|[f7964d9](https://github.com/Guillaume-Docquier/trains/commit/f7964d9a1f19dc05d1c3c6cfd8a96703614ba310)|-|-|-|-|-|
|[53deb39](https://github.com/Guillaume-Docquier/trains/commit/53deb39f9433c2bf6a989d448342b66c939b90d1)|-|-|-|-|-|
|[4e38515](https://github.com/Guillaume-Docquier/trains/commit/4e38515c1960cdc8c52431ede1f4d8cbfe9f161d)|-|-|-|-|-|
|Heuristic|00:00:00|14|54|-|A,2,0;AA,5,0;A,6,0;CC,2,1;A,2,0;DG,5,2;A,5,0;DGD,4,5;G,4,2;A,4,0;CDG,3,4;A,3,0;DG,3,5;A,3,0|

# level-3-d
| Commit | Running&nbsp;Time | Moves | Cost | Paths&nbsp;Explored | Solution |
|--------|-------------------|-------|------|--------------------:|----------|
|[a220fb0](https://github.com/Guillaume-Docquier/trains/commit/a220fb096bfd808ac96dbf479baa462d01d06d54)|06:26:48|31|78|4.262.553.487|C,3,5;D,4,3;A,2,4;DD,3,1;DDD,1,0;GC,1,3;D,1,0;CCA,2,1;CCG,2,1;D,2,0;G,3,2;CGA,3,2;DGA,3,2;D,2,0;AGD,4,3;AG,3,2;GAD,4,3;GA,3,4;DDD,3,0;CAA,5,3;DGA,5,3;DCG,5,4;D,5,4;DD,4,3;DDD,3,0;ACG,6,5;DCG,6,5;D,6,5;EGD,6,5;EG,5,4;DDD,5,0|
|[05d9692](https://github.com/Guillaume-Docquier/trains/commit/05d96929057da4e9431f275ded44df76b4a069ad)|00:10:00+|-|-|74.156.831|-|
|[a85beac](https://github.com/Guillaume-Docquier/trains/commit/a85beacb22799438f6aa498255c8482bbe330e46)|-|-|-|-|-|
|[9a9cb95](https://github.com/Guillaume-Docquier/trains/commit/9a9cb95d49e3c02ff6ec0aa410686f16d2d2f370)|-|-|-|-|-|
|[d36e848](https://github.com/Guillaume-Docquier/trains/commit/d36e84858c07b6bfa1f6ba574604eedd40b3052c)|-|-|-|-|-|
|[dfe3107](https://github.com/Guillaume-Docquier/trains/commit/dfe31079048f41b877707492fcf33d504c494529)|-|-|-|-|-|
|[f7964d9](https://github.com/Guillaume-Docquier/trains/commit/f7964d9a1f19dc05d1c3c6cfd8a96703614ba310)|-|-|-|-|-|
|[53deb39](https://github.com/Guillaume-Docquier/trains/commit/53deb39f9433c2bf6a989d448342b66c939b90d1)|-|-|-|-|-|
|[4e38515](https://github.com/Guillaume-Docquier/trains/commit/4e38515c1960cdc8c52431ede1f4d8cbfe9f161d)|-|-|-|-|-|
|Heuristic|00:00:00|31|123|-|D,1,0;D,4,0;G,1,2;C,1,4;D,1,0;CG,4,1;D,4,0;GA,4,1;D,4,0;C,3,4;D,3,0;GA,3,4;D,3,0;GA,3,4;D,3,0;AA,5,3;D,5,0;GA,5,3;D,5,0;CG,5,3;D,5,0;ACG,6,5;D,6,0;CG,6,5;D,6,0;EG,6,5;D,6,0;GAC,2,6;CAC,2,6;CG,2,6;D,2,0|