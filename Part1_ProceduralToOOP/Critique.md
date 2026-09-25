# Critique of `order_system.cpp` (Task 1.1)

## 1. Everything is global, mutable state
`customerCount`, `customerIds`, `productPrices`, `orderLineCounts`, and around fifteen
other arrays/counters are declared at file scope. Any function in the program — and any
function added later — can read or write them directly, in any order, with no
gatekeeping. There is no way to guarantee an object is left in a valid state, because
"the object" doesn't really exist; it's just a set of array slots that happen to share
an index. Two developers touching this file at the same time, or one function forgetting
to update `orderLineCounts[orderIndex]` after writing into `lineProductIndexes`, can
silently desynchronize the arrays, and the compiler has no way to catch it.

## 2. Parallel arrays stand in for objects
A "customer" isn't a thing in this program — it's `customerIds[i]`, `customerNames[i]`,
`customerEmails[i]`, `customerCities[i]`, and `customerIsVip[i]` all agreeing to use the
same `i`. Nothing enforces that agreement. If a future function inserts into
`customerNames` without inserting into `customerIds` (or inserts in a different order),
every customer after that point silently points at the wrong data, and there's no
compiler error, no exception — just wrong output discovered later.

## 3. Two incompatible notions of "which one" — id vs. index
Some functions take a business **id** (`addLineToOrder(int orderId, ...)`), others take
a raw **array index** (`calculateOrderTotal(int orderIndex)`). Reading the code requires
constantly tracking which flavor a given `int` parameter is, and passing an id where an
index is expected (or vice versa) compiles cleanly and silently reads/writes the wrong
customer, product, or order — a bug the type system is completely blind to.

## 4. Fixed-capacity arrays impose arbitrary limits and silent failure
`MAX_CUSTOMERS`, `MAX_PRODUCTS`, `MAX_ORDERS`, and `MAX_LINES_PER_ORDER` are compile-time
constants. The 51st customer, the 101st order, or the 21st line on one order doesn't
crash — it prints an error to `cout` and the caller's return value (`void`, or `-1` for
`createOrder`) has to be checked by convention, not enforced by the compiler. Nothing
stops a caller from ignoring `createOrder`'s `-1` and calling `addLineToOrder` with a
non-existent order id anyway.

## 5. Business logic is fused to console I/O
Validation, state changes, *and* `cout <<` error messages all live inside the same
functions (e.g. `addLineToOrder`). There's no way to reuse this logic behind a web API,
a test suite, or a GUI without dragging `iostream` along and re-parsing printed text to
know whether something succeeded. It also makes the functions effectively impossible to
unit test: "did `addLineToOrder` correctly reject a negative quantity" can currently
only be verified by scraping stdout.

## 6. Inconsistent, unenforceable error handling
Some failures return `-1` (`createOrder`, `findXIndexById`), some return silently
(`void` functions after printing an error), and none throw or return a typed
result/error. A caller who forgets to check `createOrder`'s return value has no other
signal that anything went wrong — the program just continues with bad state.

## 7. Hidden business rules with no single owner
The VIP 10% discount is a bare `0.90` multiplier buried inside `calculateOrderTotal`.
Nothing about the `Customer`-shaped data suggests it should affect an `Order`'s total,
and if the discount rule ever changes (a different percentage per VIP tier, say) there's
exactly one line to find, in a function that looks like it's only doing arithmetic.
Rules like this are easy to lose track of when they live disconnected from the data they
describe.

## 8. No encapsulation means no real invariants
Because every array is a global variable, nothing prevents code anywhere in the program
(now or in a future feature) from writing `orderIsPaid[3] = true;` directly, skipping
`markOrderPaid`'s guard that an order must have at least one line before it can be paid.
The "rule" only exists as long as every caller remembers to go through the one function
that enforces it — the language gives no guarantee.
