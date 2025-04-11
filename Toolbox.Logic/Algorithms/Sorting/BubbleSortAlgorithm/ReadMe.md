# Bubble Sort – Linear (Iterative) Approach

## 🧠 Core Idea

Bubble Sort works by **repeatedly stepping through the list**, comparing **adjacent elements**, and swapping them if they’re in the wrong order. This process continues until the entire list is sorted.

Each pass through the list "bubbles up" the largest unsorted element to its correct position.

### 👁️ Visual Flow

1. Start at the beginning of the list.
2. Compare each pair of adjacent elements.
3. If they’re in the wrong order, swap them.
4. Repeat the process, ignoring the sorted elements at the end each time.
5. Stop when no swaps are made during a full pass.

---

## ⏱️ Time Complexity

| Scenario      | Time       |
|---------------|------------|
| Best Case     | O(n)       |
| Average Case  | O(n²)      |
| Worst Case    | O(n²)      |

- Best case occurs when the list is already sorted (with optimization to skip unnecessary passes).

---

## 💾 Space Complexity

- **O(1)** – Bubble Sort is **in-place**, requiring no extra memory.

---

## 📌 When to Use

✅ **Good for:**
- Educational purposes
- Understanding sorting logic
- Small or nearly-sorted datasets
- Stable sorting needs (preserves the order of equal elements)

❌ **Avoid for:**
- Large datasets
- Performance-critical applications

---
---

# Bubble Sort – Recursive Approach

## 🧠 Core Idea

Recursive Bubble Sort mimics the logic of the iterative version but replaces the outer loop with recursive calls.

### 👁️ Visual Flow

1. **Base Case**  
   If the list has 0 or 1 elements, it is already sorted.

2. **Recursive Step**  
   - Perform one full pass to compare and swap adjacent elements.
   - The largest element "bubbles" to the end.
   - Recursively call the algorithm on the sublist excluding the last element (which is already in the correct position).

This process repeats until the base case is reached.

---

## ⏱️ Time Complexity

| Scenario      | Time       |
|---------------|------------|
| Best Case     | O(n²)      |
| Average Case  | O(n²)      |
| Worst Case    | O(n²)      |

Even in the best case, unless you optimize with an early-exit flag, recursive Bubble Sort will still perform unnecessary calls.

---

## 💾 Space Complexity

- **O(n)** – due to the **call stack** created by recursion  
Each recursive call adds a new frame to the call stack, with a maximum depth of `n`.

---

## 📌 When to Use

✅ **Good for:**
- Learning recursion
- Practicing algorithm-to-recursion conversion
- Small input sizes

❌ **Avoid for:**
- Large datasets
- Performance-critical or memory-sensitive applications
- Production code

Recursive Bubble Sort is mainly useful as an **educational tool**, not a practical solution.

