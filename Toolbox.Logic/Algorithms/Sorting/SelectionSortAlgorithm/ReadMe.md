## 🔁 Linear (Iterative) Approach

### 🧠 Core Idea
- Iterate through the array.
- For each position `i`, find the smallest element from index `i` to the end.
- Swap it with the element at position `i`.
- Repeat for all positions.

### 👁️ Visual Flow
1. Start at index 0.
2. Search the smallest element from index 0 to end.
3. Swap smallest element with index 0.
4. Move to index 1.
5. Repeat steps 2–4 until end of array is reached.

### ⏱️ Time Complexity
| Case        | Complexity |
|-------------|------------|
| Best Case   | O(n²)      |
| Average     | O(n²)      |
| Worst Case  | O(n²)      |

### 💾 Space Complexity
| Metric            | Value      |
|-------------------|------------|
| Auxiliary Space   | O(1)       |
| Stable?           | ❌ No      |

---
---

## 🔁 Recursive Approach

### 🧠 Core Idea
- Base case: if the current index reaches the end of the array, stop.
- Find the smallest element in the remaining unsorted portion.
- Swap it with the element at the current index.
- Recursively call the same logic on the rest of the array (index + 1 to end).

- ### 👁️ Visual Flow
1. Begin with the full array and index = 0.
2. Find the smallest element from index 0 onward.
3. Swap it with the element at index 0.
4. Call the same logic with index = 1.
5. Continue recursively until index == array length.

### ⏱️ Time Complexity
| Case        | Complexity |
|-------------|------------|
| Best Case   | O(n²)      |
| Average     | O(n²)      |
| Worst Case  | O(n²)      |

### 💾 Space Complexity
| Metric            | Value         |
|-------------------|---------------|
| Auxiliary Space   | O(n) *(stack)*|
| Stable?           | ❌ No         |

### ✅ When to Use
- Learning recursion or translating iterative logic.
- Conceptual clarity on divide-and-solve patterns.
- As a recursive exercise — **not for production performance**.

---

## ⚠️ General Notes
- Both versions are inefficient for large data.
- Neither is stable unless modified.
- Use for learning, not for real-world sorting needs.
