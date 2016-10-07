public static class EventController {
	public static void InvokeSafe (System.Action action) {
		if (action != null) {
            action();
        }
	}

	public static void InvokeSafe<T> (System.Action<T> action, T arg) {
		if (action != null) {
            action(arg);
        }
	}
}
