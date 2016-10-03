public static class EventController {
	public static void SafeInvoke (System.Action action) {
		if (action != null) {
            action();
        }
	}

	public static void SafeInvoke<T> (System.Action<T> action, T arg) {
		if (action != null) {
            action(arg);
        }
	}
}
