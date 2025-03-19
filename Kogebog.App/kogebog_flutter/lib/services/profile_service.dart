import 'dart:convert';

import 'package:kogebog_flutter/models/profile.dart';
import 'package:kogebog_flutter/services/dio_service.dart';
import 'package:shared_preferences/shared_preferences.dart';

class ProfileService {
  
  String baseUrl = '/profile';

  Future<Profile?> create(Profile profile) async {
    try {
      final response = await DioService.dio.post(baseUrl, data: profile.toJson());
      if (response.statusCode == 200) {
        final prefs = await SharedPreferences.getInstance();
        await prefs.setString('profile_id', response.data['id']);
        await prefs.setString('profile_name', response.data['name']);
        return Profile.fromJson(response.data);
      }
    } catch (e) {
      print("Error creating profile: $e");
    }
    return null;
  }
}