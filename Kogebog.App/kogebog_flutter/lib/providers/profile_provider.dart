import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:kogebog_flutter/models/profile.dart';
import 'package:kogebog_flutter/services/profile_service.dart';
import 'package:shared_preferences/shared_preferences.dart';

final profileProvider = StateNotifierProvider<ProfileNotifier, String?>((ref) {
  return ProfileNotifier();
});

class ProfileNotifier extends StateNotifier<String?> {
  ProfileNotifier() : super(null) {
    _loadProfile();
  }

  Future<void> _loadProfile() async {
    final prefs = await SharedPreferences.getInstance();
    state = prefs.getString('profile_id');
  }

  Future<void> createProfile(Profile newProfile) async {
    final profile = await ProfileService().create(newProfile);
    if (profile != null) {
      state = profile.id;
    }
  }
}
