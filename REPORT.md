# REPORT.md - Individual Assignment

Keep this concise and specific to your implementation.

## 1. Initial analysis

- How are locations and connections represented in the baseline?
Connection is a class that stores which location it came from and where it connects, as well as a travelcost. 
Location is a class that contains an id, name, position and type. 
Connections and Locations are stored as arrays in the WorldData class.

- Which operations appear frequent?
Linear searches appear in the WorldQueries class.

- Which operation(s) do you predict may scale poorly?
Linear search scales poorly because in the worst case scenario it has to iterate through all data.

- What will you investigate first?
Optimizing the search algorithm. 

## 2. Data representation

Describe important changes/retained choices, operations affected, and time/memory trade-offs.

## 3. Algorithms and adaptation

Explain your route finder at a useful high level and how the travel-budget query adapts the same ideas.

## 4. Complexity

| Operation | Your representation/algorithm | Complexity | Short explanation |
| --- | --- | --- | --- |
| Find location by ID | O(n) | | |
| Get outgoing connections | O(n) | | |
| Priority queue enqueue | O(v+e)n | | |
| Priority queue dequeue-min | O(v+e)n - 1 | | |
| Route finding | | | |

## 5. Correctness and testing

Important tests/edge cases and what they demonstrate.

## 6. Performance investigation

Record reproducible before/after measurements for loading/preparation, repeated runtime work, and relevant allocation behavior. Explain what changed and why.

| Operation/Workload | Before | After | Expected Complexity | Reason/interpretation |
| --- | --- | --- | --- | --- |
| Locations | 2500 | | | |
| Connections | 9800 | | | |
| Iterations | 5000 | | | |
| Elapsed1 | 2 ms | | | | 
| Elapsed2 | 424 ms | | | | 
| Checksum | 6267100 | | | |


## 7. Development reflection

Describe one bug, wrong assumption, failed approach, or surprising result and how you investigated it.

## 8. Tools and AI assistance

Briefly describe relevant assistance, if any.

## 9. VG extension

Only if attempting VG: heuristic, traversal constraints, correctness, and Dijkstra/A* comparison.
