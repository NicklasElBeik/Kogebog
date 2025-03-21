import 'package:kogebog_flutter/models/unit.dart';
import 'package:kogebog_flutter/services/dio_service.dart';

class UnitService {
  
  String baseUrl = '/unit';

  Future<List<Unit>> getAll() async {
    try {
      final response = await DioService.dioJson.get(baseUrl);
      
      if (response.statusCode == 200) {
        return (response.data as List).map((json) => Unit.fromJson(json)).toList();
      }
    }
    catch (e) {
      print("Error fetching units: $e");
    }

    return [];
  }
}