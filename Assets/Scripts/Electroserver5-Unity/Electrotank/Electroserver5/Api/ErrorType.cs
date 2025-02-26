namespace Electrotank.Electroserver5.Api
{
	public enum ErrorType
	{
		UserNameExists = 0,
		UserAlreadyLoggedIn = 1,
		InvalidMessageNumber = 2,
		InboundMessageFailedValidation = 3,
		MaximumClientConnectionsReached = 4,
		ZoneNotFound = 5,
		RoomNotFound = 6,
		RoomAtCapacity = 7,
		RoomPasswordMismatch = 8,
		GatewayPaused = 9,
		AccessDenied = 10,
		RoomVariableLocked = 11,
		RoomVariableAlreadyExists = 12,
		DuplicateRoomName = 13,
		DuplicateZoneName = 14,
		UserVariableAlreadyExists = 15,
		UserVariableDoesNotExist = 16,
		ZoneAllocationFailure = 17,
		RoomAllocationFailure = 18,
		UserBanned = 19,
		UserAlreadyInRoom = 20,
		LanguageFilterCheckFailed = 21,
		RegistryTransactionEncounteredError = 22,
		ActionRequiresLogin = 23,
		GenericError = 24,
		PluginNotFound = 25,
		LoginEventHandlerFailure = 26,
		InvalidUserName = 27,
		ExtensionNotFound = 28,
		PluginInitializationFailed = 29,
		EventNotFound = 30,
		FloodingFilterCheckFailed = 31,
		UserNotJoinedToRoom = 32,
		ManagedObjectNotFound = 33,
		IdleTimeReached = 34,
		ServerError = 35,
		OperationNotSupported = 36,
		InvalidLanguageFilterSettings = 37,
		InvalidFloodingFilterSettings = 38,
		ExtensionForcedReload = 39,
		UserLogOutRequested = 40,
		OnlyRtmpConnectionRemains = 41,
		GameDoesntExist = 42,
		FailedToJoinGameRoom = 43,
		GameIsLocked = 44,
		InvalidParameters = 45,
		PublicMessageRejected = 46,
		UserKickedFromServer = 47,
		LanguageFilterNotFound = 48,
		InvalidCryptoState = 49,
		FloodingFilterNotFound = 50,
		ConnectionFailed = 51,
		MultipleZonesFound = 52,
		MultipleRoomsFound = 53,
		LoginsAreDisabled = 54,
		ClientDroppedConnection = 55
	}
}
